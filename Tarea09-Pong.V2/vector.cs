using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
namespace Tarea09_Pong.V2
{
	public class vector
	{
		double cx,cy,cz;
		float r,g,b;
		public vector(){	cx=cy=cz=0;	r=g=b=1;	}
		public vector(point a, point b){
			cx = b.X-a.X;
			cy = b.Y-a.Y;
			cz = 0;
		}
		public vector(double x,double y, double z){
			cx = x;
			cy = y;
			cz = z;
		}
		
		public void Set(point a, point b){
			cx = b.X-a.X;
			cy = b.Y-a.Y;
			cz = 0;
		}
		public void Set(double x,double y, double z){
			cx = x;
			cy = y;
			cz = z;
		}
		
		public double X{	set{cx = value;}	get{return cx;}	}
		public double Y{	set{cy = value;}	get{return cy;}	}
		public double Z{	set{cz = value;}	get{return cz;}	}
		
		public void color(float R,float G,float B){	r = R;	g = G;	b = B;	}
		
		public void draw(point a, int opc){
			GL.Begin(PrimitiveType.Lines);
			GL.Color3(r,g,b);
			if (opc==2) {
				GL.Vertex2(a.X,a.Y);
				GL.Vertex2(a.X+cx,a.Y+cy);
			}
			if (opc==0) {
				for (int i = 0; i < 3; i++) {
					GL.Vertex2(a.X-i,a.Y-i);
					GL.Vertex2(a.X+cx-i,a.Y+cy-i);
				}
			}
			else{
				for (int i = 0; i < 5; i++) {
					GL.Vertex2(a.X,a.Y-i);
					GL.Vertex2(a.X+cx,a.Y+cy-i);
				}
			}
				GL.End();
		}
		public vector ProdCruz(vector v, vector w){
			return new vector(	(v.Y*w.Z)-(w.Y*v.Z), -((v.X*w.Z)-(w.X*v.Z)), (v.X*w.Y)-(w.X*v.Y)	);
		}
		public double Distancia(point a){
			double dx = Math.Pow((cx-a.Y),2);
			double dy = Math.Pow((cy-a.Y),2);
			return Math.Sqrt(dx+dy);
		}
		
		public static vector operator*(int a, vector b){
			return new vector(b.X*a,b.Y*a,b.Z*a);
		}
		
		public double ProdPunto(vector a, vector b){
			double x = a.X*b.X;
			double y = a.Y*b.Y;
			double z = a.Z*b.Z;
			return x+y+z;
		}
		
		public double Magnitud(vector a){
			double x = Math.Pow(a.X,2);
			double y = Math.Pow(a.Y,2);
			double z = Math.Pow(a.Z,2);
			return Math.Sqrt(x+y+z);
		}
		
		public double coseno(vector a, vector b){
			double aux = ProdPunto(a,b)/(Magnitud(a)*Magnitud(b));
			return aux;
		}
	}
}
