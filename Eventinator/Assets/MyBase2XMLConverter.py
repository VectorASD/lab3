import os
from zipfile import ZipFile
import json
from base64 import b64encode
import io
from xml.sax.saxutils import escape 
from PIL import Image

def secure(s): return escape(str(s), entities={"'": "&apos;", '"': "&quot;"})
def secure2(s): return json.dumps(s, ensure_ascii=False)[1:-1]
#print(secure2("yeah </> \" lolos ' ... \\ meowww КОТ"))
#exit()

def croper(name, img):
  size = len(img)
  img = Image.open(io.BytesIO(img))
  img = img.convert("RGB")
  w, h = img.width, img.height
  d = abs(w - h) // 2
  img = img.crop((d, 0, w - d, h)) if w > h else img.crop((0, d, w, h - d))
  img = img.resize((256, 256), Image.LANCZOS)
  res_img = io.BytesIO()
  img.save(res_img, format="JPEG", quality=60)
  res = res_img.getvalue()
  #Image.open(io.BytesIO(res)).show()
  print("    %s (%s x %s)    %s b. -> %s b." % (name, w, h, size, len(res)))
  return res

yeah = set()
yeah2 = set()
yeah3 = []
count = 0
with open("Storager.cs", "w", encoding="utf-8") as file2:  
  file2.write("using System.Collections.ObjectModel;\n")
  file2.write("namespace Eventinator.Models {\n")
  file2.write("  public class Storager {\n")
  #file2.write("    public static readonly ObservableCollection<CityEvent> eventsList = new() {\n")
  file2.write("    public static readonly CityEvent[] eventsList = new[] {\n")
  with open("Storager.xml", "w", encoding="utf-8") as file:  
    file.write("<Events>\n")
    for name in os.listdir():
      if not os.path.isfile(name) or not name.endswith(".zip"): continue
      catss = set()
      yeah3.append(catss)
      with ZipFile(name, "r") as zip:
        print()
        print("~" * 48, name)
        data = json.loads(zip.read("contNet.json"))
        for event in data:
          date, cats, title, (desc, price), img = event
          yeah.add(tuple(sorted(date)))
          yeah2 |= set(cats)
          catss |= set(cats)
          
          date = date["month"] if len(date) == 1 else "%s %s (%s)" % (date["day"], date["month"], date["weekday"])
          img = croper(img, zip.read("images/" + img))
          img = b64encode(img).decode("utf-8")
          grand_cat = name[5:-4]
          
          file.write("  <Event>\n")
          file.write('    <Header data="%s"/>\n' % secure(title))
          if desc: file.write('    <Description data="%s"/>\n' % secure(desc[:135]))
          file.write('    <Image data="%s"/>\n' % img)
          file.write('    <Date data="%s"/>\n' % secure(date))
          file.write('    <Category grand="%s">\n' % grand_cat)
          for cat in cats: file.write('      <CategoryItem data="%s"/>\n' % secure(cat))
          file.write("    </Category>\n")
          if price: file.write('    <Price data="%s"/>\n' % secure(price))
          file.write("  </Event>\n")
          
          file2.write('      new CityEvent("%s", "%s", "%s", "%s", new string[] {%s}, "%s", "%s"),\n' % (
            secure2(title),
            secure2(desc[:135]) if desc else "",
            img,
            secure2(date),
            ", ".join('"%s"' % cat for cat in cats),
            grand_cat,
            secure2(price) if price else ""
          ))
          count += 1
          #if count == 5: break
      #if count == 5: break
    file.write("</Events>\n")
  file2.write("    };\n  }\n}\n")
print(yeah) # Бывает только либо отшельник month, либо day + month + weekday, а третьего не дано ;'-}
print(sorted(yeah2), len(yeah2)) # Всего зацеплено 55 категорий, хоть я и пропарсил только 9 требуемых страниц
for cats in yeah3:
  print(sorted(cats))
