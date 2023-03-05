from PIL import Image
import io
from base64 import b64encode

def check_box(pos):
  for i in range(pos, pos + 26):
    if pixels[i] != black_n: return False # верхняя граница
  for i in range(pos, pos + 26 * w, w):
    if pixels[i] != black_n: return False # левая граница
  p2 = pos + 25
  for i in range(p2, p2 + 26 * w, w):
    if pixels[i] != black_n: return False # правая граница
  pos += 25 * w
  for i in range(pos, pos + 26):
    if pixels[i] != black_n: return False # нижняя граница
  return True

def png_compressor(box):
  box = box.convert("RGB") # Palette (256 colors) -> RGB
  size_img = io.BytesIO()
  box.save(size_img, format="JPEG", quality=90) # quality < 90 уже сильно врубается в глаз
  orig = len(size_img.getvalue())
  box = box.quantize(colors=16, method=Image.MAXCOVERAGE) # дизеринг в студию... RGB -> Palette (16 colors)
  assert len(box.palette.colors) == 16
  res_img = io.BytesIO()
  box.save(res_img, format="PNG")
  res = res_img.getvalue()
  print("    size: %sb. -> %sb." % (orig, len(res)))
  show = False
  if show:
    Image.open(io.BytesIO(res)).show()
    exit()
  return b64encode(res).decode("utf-8")

img = Image.open("IconMap.png") # Нужно обратить внимание на то, что здесь режим палитры
print(img)
palette = img.palette.colors
black_n = palette[(0, 0, 0)]
print("Black:", black_n)
w, h = img.size
pixels = list(img.getdata())
#print(len(pixels), w * h)
count = 0
res = []
for pos, i in enumerate(pixels):
  if i == black_n: count += 1
  elif count:
    if count == 26 and check_box(pos - 26):
      Y, X = divmod(pos - 26, w)
      print("box:", X, Y)
      box = img.crop((X + 1, Y + 1, X + 25, Y + 25))
      Bin = png_compressor(box)
      res.append(Bin)
    count = 0

for n, Bin in enumerate(res):
  print('private string img_%c = "%s";' % (chr(ord('a') + n % 26), Bin))
