using System;
using System.Drawing;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
namespace Tarea09_Pong.V2
{
	public class Screen : GameWindow
	{
		//entorno
		vector lder, lizq, lsup, linf;
		circle ball,obj1,obj2,obj3;
		int r = 24, p1=0, p2=0, p3=0;
		double _x=95, v=2, _v=2; //_x posicion de la raqueta
		//Funciones
		funciones f = new funciones();
		vector vMano = new vector(0,0,1);
		vector vColision = new vector();
		vector vRaqueta = new vector();
		vector vRaquetaPrim = new vector();
		vector vPerpendicular = new vector();
		bool vida=true;
		double delta=1, d=1;
		//ball
		vector direccion = new vector();
		bool colisionx = false;
		bool colisiony = false;
		point posxy = new point();
		//Panel de arriba
		circle c1, c2, c3;
		//
		System.Random cxy = new System.Random();
		int vcont=3;//contador de vidas
		int largo=100;//largo de la raqueta
		double inc = 0.5; //incremento si pega con un objetivo
		public Screen(int l, int w) : base(l,w)
		{
			Title = "Tarea 09 - Pong V.4 Graficacion";
		}
		protected override void OnLoad(System.EventArgs e){
			GL.ClearColor(Color.LightSkyBlue);
			GL.MatrixMode(MatrixMode.Projection);
			GL.Ortho(0,600,0,600,-1,1);
			
			//Cuadro
			lder = new vector(new point(560,560),new point(560,40));
			lizq = new vector(new point(40,560),new point(40,40));
			lsup = new vector(new point(40,560),new point(560,560));
			linf = new vector(new point(40,40),new point(560,40));
			//Objetivos
			obj1 = new circle(0,0,r);
			obj2 = new circle(0,0,r);
			obj3 = new circle(0,0,r);
			obj1.X=cxy.Next(80,350);
			obj2.X=cxy.Next((int)obj1.X,540);
			obj3.X=cxy.Next((int)obj1.X,540);
			obj3.Y=cxy.Next(200,350);
			obj2.Y=cxy.Next(300,450);
			obj1.Y=cxy.Next(400,500);
			//Raqueta
			vRaqueta.Set(largo,0,0);
			//ball
			ball = new circle();
			ball.R=10;
			posxy.Set(new point(100,300));
			direccion.Set(1,-1,0);
			//vectores
			vRaquetaPrim.Set(0,0,0); 
			vColision.Set(0,0,0);
			//Panel de arriba
			c1 = new circle(450,580,15);
			c2 = new circle(500,580,15);
			c3 = new circle(550,580,15);
		}
		protected override void OnUpdateFrame(FrameEventArgs e)
		{
			GL.Clear(ClearBufferMask.ColorBufferBit);
			//Revision de colision de la raqueta con la pelota
			if (f.tope(ball,new vector(_x,50,0),vRaqueta.Distancia(new point(_x,0)),5)){
				//ball
				ball.color(0.5f,0.2f,0.5f);
				ball.color_c(0.5f,0.2f,0.5f);
				//vector
				vColision.Set(posxy,new point(_x,50));
				vPerpendicular = vPerpendicular.ProdCruz(vMano,vRaqueta);
				
				delta = vColision.coseno(vPerpendicular,vColision);
				d = vPerpendicular.coseno(vPerpendicular,vRaqueta);
				if ((direccion.X>=-1)&&(direccion.X>0)) {
					vRaquetaPrim = vRaquetaPrim.ProdCruz(vMano,vColision);delta=delta-d;
				}
				else{
					vRaquetaPrim = vRaquetaPrim.ProdCruz(vColision,vMano);delta=d-delta;
				}
				delta = f.angulo(delta);
				vRaquetaPrim.X*=Math.Acos(delta);
				vRaquetaPrim.Y*=Math.Asin(delta);
				direccion.Set(vRaquetaPrim.X,vRaquetaPrim.Y,0);
				direccion.X=(direccion.X/direccion.Magnitud(direccion));
				direccion.Y=(direccion.Y/direccion.Magnitud(direccion));
			}
			else{
				//ball
				ball.color(1.0f,0.1f,0.3f);
				ball.color_c(1.0f,0.1f,0.3f);
			}
			//Cuadro
			lder.color(1.0f,0.8f,0.0f);
			lizq.color(0.5f,0.2f,1.0f);
			lsup.color(0.0f,0.5f,0.3f);
			linf.color(1.0f,0.2f,0.2f);
			//
			//Revision si tiene los 3 corazones
			if (vida) {
				//corazones
				switch (vcont) {
						case 3:
							corazon(100,580,1,1,1,14);
							corazon(150,580,1,1,1,14);
							corazon(200,580,1,1,1,14);
							corazon(100,580,1,0,0,12);
							corazon(150,580,1,0,0,12);
							corazon(200,580,1,0,0,12);
							//raqueta
							vRaqueta.color(0.0f,0.5f,0.5f);
							f.DrawFig(300,300,4,45,368,0.4f,0.6f,0.8f);
							GL.ClearColor(Color.LightSkyBlue);
						break;
						case 2:
							corazon(100,580,1,1,1,14);
							corazon(150,580,1,1,1,14);
							corazon(200,580,1,1,1,14);
							corazon(100,580,1,0,0,12);
							corazon(150,580,1,0,0,12);
							corazon(200,580,0,0,0,12);
							//raqueta
							vRaqueta.color(1.0f,0.5f,0.0f);
							f.DrawFig(300,300,4,45,368,0.5f,0.5f,0.8f);
							GL.ClearColor(Color.LightGray);
						break;
						case 1:
							corazon(100,580,1,1,1,14);
							corazon(150,580,1,1,1,14);
							corazon(200,580,1,1,1,14);
							corazon(100,580,1,0,0,12);
							corazon(150,580,0,0,0,12);
							corazon(200,580,0,0,0,12);
							//raqueta
							vRaqueta.color(1.0f,0.2f,0.2f);
							f.DrawFig(300,300,4,45,368,0.2f,0.2f,0.2f);
							//Cuadro
							lder.color(1,(float)Math.Sin(cxy.Next(1,255)),0.2f);
							lizq.color(1,(float)Math.Sin(cxy.Next(1,255)),0.2f);
							lsup.color(1,(float)Math.Sin(cxy.Next(1,255)),0);
							linf.color(1,(float)Math.Sin(cxy.Next(1,255)),0);
							//
							GL.ClearColor(Color.DimGray);
						break;
				}
				//Colisiones de objetivos
				if (p1==0) {
					obj1.color_c(1.0f,0.1f,0.3f);
					c1.color(0,0,0);
					p1=f.colision(ball,obj1);
					if (p1!=0) {c1.color(1.0f,0.1f,0.3f);v+=inc;r-=4;}
				}
				if (p2==0) {
					obj2.color_c(1.0f,0.1f,0.3f);
					c2.color(0,0,0);
					p2=f.colision(ball,obj2);
					if (p2!=0) {c2.color(1.0f,0.1f,0.3f);v+=inc;r-=4;}
				}
				if (p3==0) {
					obj3.color_c(1.0f,0.1f,0.3f);
					c3.color(0,0,0);
					p3=f.colision(ball,obj3);
					if (p3!=0) {c3.color(1.0f,0.1f,0.3f);v+=inc;r-=4;}
				}
				ball.Set(posxy);//Actualiza la posicion de la pelota
				
				//Movimiento entre el cuadro
				if ((ball.X+ball.R>560)||(ball.X<=ball.R+40)) {
					colisionx=true;
					ball.color(0.5f,0.2f,0.5f);
					ball.color_c(0.5f,0.2f,0.5f);
				}
				if (colisionx) {
					direccion.X=-1*direccion.X;
					colisionx=false;
				}
				if ((ball.Y+ball.R>560)||(ball.Y<=20)) {
					colisiony=true;
					ball.color(0.5f,0.2f,0.5f);
					ball.color_c(0.5f,0.2f,0.5f);
				}
				if (colisiony) {
					direccion.Y=-1*direccion.Y;
					colisiony=false;
				}
				posxy.X=posxy.X+v*direccion.X;
				posxy.Y=posxy.Y+v*direccion.Y;
			}
			//Revision si no toca la linea inferior
			if (ball.Centro.Y<=ball.R+30) {
				if (vcont==1) {
					GL.ClearColor(Color.Black);
					f.DrawFig(300,300,4,45,368,0.1f,0.1f,0.1f);
				    obj1.color_c(1.0f,0.1f,0.3f);
				    obj2.color_c(1.0f,0.1f,0.3f);
				    obj3.color_c(1.0f,0.1f,0.3f);
				    obj1.color(0,0,0);
				    obj2.color(0,0,0);
				    obj3.color(0,0,0);
				    vRaqueta.color(0f,0f,0f);
				    corazon(100,580,1,1,1,14);
					corazon(150,580,1,1,1,14);
					corazon(200,580,1,1,1,14);
				    corazon(100,580,0.8f,0.5f,0.5f,12.5);
					corazon(150,580,0.8f,0.5f,0.5f,12.5);
					corazon(200,580,0.8f,0.5f,0.5f,12.5);
				    vida=false;v=0;
				}
				else{
					posxy.Set(new point(100,300));
					direccion.Set(1,-1,0);
					vcont--;largo=largo-(largo/4);
				}
			}
		}
		protected override void OnRenderFrame(FrameEventArgs e){
		//Entorno
			//Objetivos
			if ((p1!=0)&&(p2!=0)&&(p3!=0)) {
				GL.ClearColor(Color.LightGreen);
				f.DrawFig(300,300,4,45,368,0.2f,0.8f,0.6f);
				//Cuadro
				lsup.draw(new point(40,560),0);
				linf.draw(new point(40,40),0);
				lder.draw(new point(560,560),0);
				lizq.draw(new point(40,560),0);v=0;
			}
			else{
				//Cuadro
				lsup.draw(new point(40,560),0);
				linf.draw(new point(40,40),0);
				lder.draw(new point(560,560),0);
				lizq.draw(new point(40,560),0);
				obj1.R=obj2.R=obj3.R=r;
				obj1.DrawC();
				obj2.DrawC();
				obj3.DrawC();
			}
			vRaqueta.Set(largo,0,0);
			vRaqueta.draw(new point(_x,50),1);
			c1.DrawC();
			c2.DrawC();
			c3.DrawC();
			ball.DrawC();
//			vColision.draw(new point((_x+50/2),100),2);
//			vRaquetaPrim.draw(new point((_x+50/2),100),2);
//			vPerpendicular.draw(new point((_x+50/2),100),2);
			//
			SwapBuffers();
		}
		protected override void OnKeyPress(KeyPressEventArgs e){
			if (e.KeyChar == 'q' || e.KeyChar == 'Q') { Exit(); }
			if (e.KeyChar == 's' || e.KeyChar == 'S') {
				if (_x+largo<555) {
					_x+=15;
				}
			}
			if (e.KeyChar == 'a' || e.KeyChar == 'A') {
				if (_x>45) {
					_x-=15;
				}
			}
			if (e.KeyChar == '1') {_v=v;v=0;}
			if (e.KeyChar == '2') {v=_v;}
			if (e.KeyChar == '3') {
				GL.ClearColor(Color.LightSkyBlue);
				f.DrawFig(300,300,4,45,368,0.4f,0.6f,0.8f);
				vida=true;v=1;p1=p2=p3=0;
				v=2;largo=100;
				obj1.color_c(1.0f,0.1f,0.3f);
				obj2.color_c(1.0f,0.1f,0.3f);
				obj3.color_c(1.0f,0.1f,0.3f);
				obj1.color(1,1,1);
				obj2.color(1,1,1);
				obj3.color(1,1,1);
				obj1.X=cxy.Next(80,350);
				obj2.X=cxy.Next((int)obj1.X,540);
				obj3.X=cxy.Next((int)obj1.X,540);
				obj3.Y=cxy.Next(200,350);
				obj2.Y=cxy.Next(300,450);
				obj1.Y=cxy.Next(400,500);
				vRaqueta.color(0.0f,0.5f,0.5f);
				posxy.Set(new point(100,300));
				direccion.Set(1,-1,0);
				_x=250;vcont=3;r=24;
				vRaqueta.Set(largo,0,0); 
				c1.color(0,0,0);
				c2.color(0,0,0);
				c3.color(0,0,0);
			}
		}
		protected override void OnMouseMove(MouseMoveEventArgs e){
//			if ((e.X>40)&&(e.X+largo<560)) {
//				vRaqueta.Set(new point(e.X,50),new point(e.X+largo,50));
//				_x=e.X;
//			}
		}
		public void corazon(double x, double y, float r, float g, float b, double tam){
			f.DrawFig(x,y,4,0,tam-0.5,r,g,b);
			f.DrawFig(x+(tam-tam/2),y+tam/2,360,0,tam-tam/3,r,g,b);
			f.DrawFig(x-(tam-tam/2),y+tam/2,360,0,tam-tam/3,r,g,b);
		}
	}
}