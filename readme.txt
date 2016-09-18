version 2.0 
date: 2016-9-16
	增加了将图片颜色级数降低的功能
	新增class ColorProcess(暂名) 来对图像进行处理
	Picture的作用是叫出ColorProcess

version 2.5
date: 2016-9-18 21:52:32
	ColorProcess 更名为 PixelProcess
	PixelProcess 存放的是对单个像素进行处理的方法
	新增 ScaleProcess
	ScaleProcess 存放的是将图片伸缩的方法(该类方法无法直接在原图上进行操作)
	增加了在窗口上用PictureBox显示图片
	现在支持使用滚动条了
	Class Show 暂时还没有作用