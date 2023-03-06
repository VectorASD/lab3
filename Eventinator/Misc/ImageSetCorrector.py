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
