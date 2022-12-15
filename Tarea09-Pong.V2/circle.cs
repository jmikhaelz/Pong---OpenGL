using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
namespace Tarea09_Pong.V2
{
	public class circle
	{
		double cx,cy,r;
		float ro,g,b;
		float r_c, g_c, b_c;
		public circle(){	cx=cy=r=0; ro=g=b=1; r_c=g_c=b_c=1;	}
		
		public circle(double x, double y, double ra){	cx=x;cy=y;r=ra;	ro=g=b=1;	r_c=g_c=b_c=1;	}
		public void Set(double x, double y, double ra){	cx=x;cy=y;r=ra;	}
		
		public double X{	set{cx = value;}	get{return cx;}	}
		public double Y{	set{cy = value;}	get{return cy;}	}
		public double R{	set{r = value;}	get{return r;}	}
		public double D{	set{r = value/2;}	get{return r*2;}	}
		public point Centro{	set{cx = value.X;cy = value.Y;}	get{return new point(cx,cy);}	}
		public void Set(point z){	cx=z.X;cy=z.Y;	}
		
		public void color(float R,float G,float B){	ro = R;	g = G;	b = B;	}
		public void color_c(float R,float G,float B){	r_c = R;	g_c = G;	b_c = B;	}
		
		public void DrawC(){
			GL.Begin(PrimitiveType.Polygon);
			GL.Color3(ro,g,b);
			for (double i = 0; i < Math.PI*2; i+=0.1) {
				GL.Vertex2(cx+Math.Cos(i)*r,cy+Math.Sin(i)*r);
			}
			GL.End();
			GL.Begin(PrimitiveType.Points);
			GL.Color3(r_c,g_c,b_c);
			for (double j = -r; j < r; j+=(r/4)) {
				for (double i = 0; i < Math.PI*2; i+=0.01) {
				    GL.Vertex2(cx+Math.Cos(i)*j,cy+Math.Sin(i)*j);
			    }
			}
			for (int i = 0; i <= (r/10); i++) {
				GL.Vertex2(cx+i,cy);
				GL.Vertex2(cx,cy+i);
				GL.Vertex2(cx-i,cy);
				GL.Vertex2(cx,cy-i);
			}
			GL.End();
		}
	}
}
