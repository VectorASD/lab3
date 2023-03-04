import os
from zipfile import ZipFile
import json
from base64 import b64encode
import io
from xml.sax.saxutils import escape 

#def secure(s): return json.dumps(s)[1:-1]
def secure(s): return escape(str(s), entities={"'": "&apos;", '\"': "&quot;"})
#print(secure("yeah </> \" lolos ' ... \\ meowww КОТ"))
#exit()

yeah = set()
yeah2 = set()
count = 0
with open("Storager.xml", "w", encoding="utf-8") as file:
  file.write("<Events>\n")
  for name in os.listdir():
    if not os.path.isfile(name) or not name.endswith(".zip"): continue
    with ZipFile(name, "r") as zip:
      print()
      print("~" * 48, name)
      data = json.loads(zip.read("contNet.json"))
      for event in data:
        date, cats, title, (desc, price), img = event
        print("   ", img)
        yeah.add(tuple(sorted(date)))
        yeah2 |= set(cats)
        
        date = date["month"] if len(date) == 1 else "%s %s (%s)" % (date["day"], date["month"], date["weekday"])
        img = b64encode(zip.read("images/" + img)).decode("utf-8")
        
        file.write("  <Event>\n")
        file.write('    <Header data="%s"/>\n' % secure(title))
        if secure(title).count('"'): print(secure(title))
        if desc: file.write('    <Description data="%s"/>\n' % secure(desc[:135]))
        file.write('    <Image data="%s"/>\n' % img)
        file.write('    <Date data="%s"/>\n' % secure(date))
        file.write("    <Category>\n")
        for cat in cats: file.write('      <CategoryItem data="%s"/>\n' % secure(cat))
        file.write("    </Category>\n")
        if price: file.write('    <Price data="%s"/>\n' % secure(price))
        file.write("  </Event>\n")
        count += 1
        if count == 5: break
    if count == 5: break
  file.write("</Events>\n")
print(yeah) # Бывает только либо отшельник month, либо day + month + weekday, а третьего не дано ;'-}
print(yeah2, len(yeah2)) # Всего зацеплено 55 категорий, хоть я и пропарсил только 9 требуемых страниц
