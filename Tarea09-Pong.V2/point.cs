using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
namespace Tarea09_Pong.V2
{
	public class point
	{
		double x,y;
		public point(){	x=y=0;	}
		public point(double x_, double y_){	x=x_;	y=y_;	}
		public void Set(double x_, double y_){	x=x_;	y=y_;	}
		public void Set(point a){	x=a.X;	y=a.Y;	}
		public double X{	set{x = value;}	get{return x;}	}
		public double Y{	set{y = value;}	get{return y;}	}
		
		public void draw(){
			GL.PointSize(5);
			GL.Begin(PrimitiveType.Points);
			GL.Vertex2(x,y);
			GL.End();
		}
	}
}
