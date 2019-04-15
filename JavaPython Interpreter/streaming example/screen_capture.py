import wx, time
app = wx.App()  # Need to create an App instance before doing anything
screen = wx.ScreenDC()
size = screen.GetSize()
bmp = wx.EmptyBitmap(size[0], size[1])
while True:
	mem = wx.MemoryDC(bmp)
	mem.Blit(0, 0, size[0], size[1], screen, 0, 0)
	del mem  # Release bitmap
	try:
		bmp.SaveFile("G:\\Github Repository\\iggy12345s_vault\\JavaPython Interpreter\\streaming example\\var\\img\\image.png", wx.BITMAP_TYPE_PNG)
	except:
		print("Something went wrong while saving the file")
	time.sleep(0.1)