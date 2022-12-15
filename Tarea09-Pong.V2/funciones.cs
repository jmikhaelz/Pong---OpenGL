using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
namespace Tarea09_Pong.V2
{
	public class funciones
	{
		public funciones(){}
		public int colision(circle c1,circle c2){
			double Vx = (c1.X-c2.X);
			double Vy = (c1.Y-c2.Y);
			double distancia = Math.Sqrt(Math.Pow(Vx,2)+Math.Pow(Vy,2));
			if (distancia < (c1.R+c2.R)) {
				c2.color(0.0f,0.0f,0.0f);
				c2.color_c(0.0f,0.0f,0.0f);
				return 1;
			}
			return 0;
		}
		public bool tope(circle ball, vector raqueta, double d, double rh){//colision de circulo con rectangulo
			double dcx = Math.Abs(ball.Centro.X-raqueta.X-(d/2));
			double dcy = Math.Abs(ball.Centro.Y-raqueta.Y-(rh/2));
			if ((dcx > (ball.R+(d/2))) || (dcy > (ball.R+(rh/2)))){
				return false;
			}
			else if ((dcx <= (d/2))	|| (dcy <= (rh/2))){
				return true;
			}
			return false;
		}
		public double rad(double grado){
			return (grado/180)*Math.PI;
		}
		public double angulo(double rad){
			return (rad/Math.PI*2);
		}
		public void Div(int d, double tam){
			  	GL.Begin(PrimitiveType.Lines);
				GL.Color3(0.1f, 0.2f, 0.2f);
			for (double i = -tam; i < tam; i+=(tam/d)) {
				GL.Vertex2(i,-1*i);
				GL.Vertex2(i,tam);
				GL.Vertex2(-1*i,i);
				GL.Vertex2(tam,i);
			}
				GL.End();
		}
		public void DrawFig(double _x, double _y, double lados, double angulo, double tam, float r, float g, float b){
			double inc = (Math.PI*2/lados); 		//Dividimos los lados entra la circuferencias 2PI/l
			double ang = (angulo/180)*Math.PI;		//Convertimos angulo en radianes (a/180)PI
			GL.Color3(r,g,b);
			GL.Begin(PrimitiveType.Polygon);
			for (double x = ang; x <= (Math.PI*2)+ang; x+=inc) {
   				GL.Vertex2(_x+Math.Cos(x-inc)*tam,_y+Math.Sin(x-inc)*tam);
   				GL.Vertex2(_x+Math.Cos(x)*tam,_y+Math.Sin(x)*tam);
  			}
			GL.End();
		}
		
	}
}
