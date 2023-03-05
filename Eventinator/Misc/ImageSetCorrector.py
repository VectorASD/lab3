# А вот эта штука используется после консольной программы Воровалка.js
# Вытряхивает все картинки, что не могла позволить консольная прога из-за крайне "любимого" всеми нами CORS
# Если выключить во время работы, то от гибель зипки даже не спасёт with as конструкция, учтите ;'-}
# Пока не было случая, чтобы была картинка, что не возможно скачать, так что результат оказался полноценным!

import os
from zipfile import ZipFile
import json
import requests

sess = requests.Session()

for name in os.listdir():
  if not os.path.isfile(name) or not name.endswith(".zip"): continue
  with ZipFile(name, "r") as zip:
    trouble = "Oh_what_a_veryveryvery_bad_boy.json"
    names = set(zip.namelist())
    if trouble not in names:
      zip.close()
      continue
    data = zip.read(trouble)
  print()
  print("~" * 72, name)
  with ZipFile(name, "a") as zip:
    for name, url in json.loads(data).items():
      name = "images/" + name
      if name in names:
        print(name, "exists")
        continue
      print(name, url)
      resp = sess.get(url)
      if not resp.ok: print("Fail load image", name, "->", url)
      zip.writestr(name, resp.content, compresslevel = 9)
      #break
  #break
