using System;
using System.Runtime.InteropServices;

namespace BaseLibS.Graph.Image{
	public sealed unsafe class ColorPixelAccessor : IPixelAccessor{
		private Color2* pixelsBase;
		private GCHandle pixelsHandle;
		private bool isDisposed;
		public ColorPixelAccessor(IImageBase image){
			if (image == null){
				throw new ArgumentNullException();
			}
			if (image.Width <= 0 || image.Height <= 0){
				throw new ArgumentOutOfRangeException();
			}
			Width = image.Width;
			Height = image.Height;
			pixelsHandle = GCHandle.Alloc(((ImageBase) image).Pixels, GCHandleType.Pinned);
			pixelsBase = (Color2*) pixelsHandle.AddrOfPinnedObject().ToPointer();
		}
		~ColorPixelAccessor(){
			Dispose();
		}
		public int Width { get; }
		public int Height { get; }
		public Color2 this[int x, int y]{
			get{
				return *(pixelsBase + (y*Width + x));
			}
			set{
				*(pixelsBase + (y*Width + x)) = value;
			}
		}
		public void Dispose(){
			if (isDisposed){
				return;
			}
			if (pixelsHandle.IsAllocated){
				pixelsHandle.Free();
			}
			pixelsBase = null;

			isDisposed = true;
			GC.SuppressFinalize(this);
		}
	}
}