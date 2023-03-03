// Написал простенький скриптик за 300 часиков для сверхзвукового слива всех внутренностей https://gorodzovet.ru/ сайта.
// Засовывать это чудо в консоль. Сливается конкретно содрежимое текущей страницы.
// Картинки оно скачивать теперь тоже умеет само. НО! Придётся докачать через python скрипт.

let concentrator = [];
let images = {};
function RandName() { return (Math.random() + 1).toString(36).substring(3, 7); }
for (let event of document.getElementById("events").children) {
    //console.log(event)
    let date = {}, tags = [];
    for (let el of event.getElementsByClassName("event-block-date")[0].children) {
        let text = [];
        for (let i of el.textContent.split("-")) text.push(i.trim());
        date[el.classList.value] = text.join(" - ");
    }
    for (let el of event.getElementsByClassName("event-tags")[0].children)
        tags.push(el.textContent.trim());
    let title = event.getElementsByClassName("event-block-title")[0].textContent.trim();
    let extra = event.getElementsByClassName("event-block-extra")[0];
    let ex_a = extra.getElementsByTagName("small")[0];
    let ex_b = extra.getElementsByTagName("b")[0];
    if (ex_a) ex_a = ex_a.textContent.trim();
    if (ex_b) ex_b = parseInt(ex_b.textContent.trim());
    extra = [ex_a, ex_b];
    let img = event.getElementsByTagName("img")[0], name;
    while (1) {
        name = RandName() + ".jpg"
        if (!images[name]) break;
    }
    let block = [date, tags, title, extra, name];
    concentrator.push(block);
    images[name] = img;
}
function Save() { // Устарело из-за использования ZIP-накопителя
    let blob = new Blob([new TextEncoder().encode(JSON.stringify(concentrator))], { type: "application/json;charset=utf-8" });
    let name = location.pathname.split("/");
    name = name[name.length - 2];
    let Node = document.createElement("a");
    Node.target = "_blank";
    Node.download = "yeah_" + name + ".json";
    Node.href = URL.createObjectURL(blob);
    setTimeout(() => Node.click());
}
if (!Script) var Script = { // Рассчитано, что на сайте уже стоит jQuery lib
    _loadedScripts: [],
    load: function (script) {
        if (this._loadedScripts.indexOf(script) != -1) return false;
        console.log("Подключаем " + script)
        let code = $.ajax({ async: false, url: script }).responseText;
        (window.execScript || window.eval)(code);
        this._loadedScripts.push(script);
        //$$("head").first().insert(Object.extend(
        //  new Element("script", {type: "text/javascript"}), {text: code}
        //));
    }
};
async function ImgToBlob(img) {
    //img.crossOrigin = "Anonymous";
    //img.src += " ";
    let blob;
    try { blob = await fetch(img.src, { mode: "cors" }).then(r => r.blob()); }
    catch (e) { blob = await fetch(img.src, { mode: "no-cors" }).then(r => r.blob()); }
    if (blob.size == 0) return null;
    return blob;
    let canvas = document.createElement('canvas');
    canvas.width = img.clientWidth;
    canvas.height = img.clientHeight;
    let ctx = canvas.getContext('2d');
    ctx.drawImage(img, 0, 0);
    let blobb = await new Promise(resolve => canvas.toBlob(resolve, 'image/png'));
    return blobb;
}
async function Imager() {
    let zip = new JSZip();
    let img = zip.folder("images");
    let badbadbad_cors = {}, failboy = false;
    for (let name in images) {
        let node = images[name];
        let blob = await ImgToBlob(node);
        if (blob != null) {
            console.log(name + " loaded: " + blob.size + "b.");
            img.file(name, blob, { base64: true });
        } else {
            console.log(name + " cors-error :/");
            badbadbad_cors[name] = node.src;
            failboy = true;
        }
    }
    zip.file("contNet.json", JSON.stringify(concentrator));
    if (failboy) zip.file("Oh_what_a_veryveryvery_bad_boy.json", JSON.stringify(badbadbad_cors));
    //console.log(zip);
    zip.generateAsync({ type: "blob" }).then(function (content) {
        let name = location.pathname.split("/");
        name = name[name.length - 2];
        console.log("final:", content);
        saveAs(content, "yeah_" + name + ".zip");
    });
}
Script.load("https://raw.githubusercontent.com/VectorASD/Storage/main/jszip.min.js");
Script.load("https://raw.githubusercontent.com/VectorASD/Storage/main/FileSaver.min.js");
if (!!window.JSZip && !!window.saveAs) {
    await Imager();
    //Save();
} else console.log("Ошибка загрузки зипера, либо saveAs функции");