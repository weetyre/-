using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 站场图Practice
{
    public partial class Field : Form
    {
        //以下的功能目的是为了调取系统的异或画笔
        [DllImport("Gdi32.dll")]

        static extern IntPtr CreatePen(int fnPenStyle, int width, int color);

        [DllImport("Gdi32.dll")]

        static extern int SetROP2(System.IntPtr hdc, int rop);
        [DllImport("Gdi32.dll")]

        static extern int MoveToEx(IntPtr hdc, int x, int y, IntPtr lppoint);

        [DllImport("Gdi32.dll")]
        static extern int LineTo(IntPtr hdc, int X, int Y);

        [DllImport("Gdi32.dll")]

        static extern IntPtr SelectObject(IntPtr hdc, IntPtr obj);

        public int count = 0;//SnP_8
        public int count1 = 0;//Snp_3
        public int count2 = 0;//Snp_6
        public int count3 = 0;//Snp_5
        public int count4 = 0;//Snp_4
        public int count5 = 0;//Xnp_3
        public int count6 = 0;//Xnp_4
        public int count7 = 0;//Xnp_5
        public int count8 = 0;//Xnp_6
        public int count9 = 0;//Xnp_8
        public int count10 = 0;//Xp
        public int count11 = 0;//Sp

        public int s8count = 0;//Snp_8
        public int s6count = 0;//snp_6
        public int s3count = 0;//snp_3
        public int s5count = 0;//snp_5
        public int s4count = 0;//snp_4
        public int x3count = 0;//xnp_3
        public int x5count = 0;//xnp_5
        public int x4count = 0;//xnp_4
        public int x6count = 0;//xnp_6
        public int x8count = 0;//xnp_8
        public int xcount = 0;//xp
        public int scount = 0;//sp

        public int drt;
        Panel front = new Panel();
        Panel back = new Panel();
        DateTime dt = new DateTime(2018, 9, 20, 8, 20, 00);
        System.Windows.Forms.Timer trainTimer = new System.Windows.Forms.Timer();
        Point[] snake = new Point[12];
        int second = 0;

        //信号灯位置点
        Point Xred = new Point(37, 240);
        Point XGreen = new Point(42, 240);
        Point X3red = new Point(607, 190);
        Point X3Green = new Point(612, 190);
        Point XFred = new Point(42, 300);
        Point XFGreen = new Point(47, 300);
        Point X1red = new Point(747, 240);
        Point X1Green = new Point(752, 240);
        Point X2red = new Point(747, 290);
        Point X2Green = new Point(752, 290);
        Point X4red = new Point(657, 340);
        Point X4Green = new Point(662, 340);
        Point X6red = new Point(597, 390);
        Point X6Green = new Point(602, 390);
        Point X8red = new Point(552, 440);
        Point X8Green = new Point(557, 440);
        Point X5red = new Point(607, 140);
        Point X5Green = new Point(612, 140);

        Point S1red = new Point(233, 250);
        Point S1Green = new Point(226, 250);
        Point S2red = new Point(233, 300);
        Point S2Green = new Point(226, 300);
        Point S3red = new Point(384, 200);
        Point S3Green = new Point(377, 200);
        Point S4red = new Point(313, 355);
        Point S4Green = new Point(306, 355);
        Point S6red = new Point(393, 405);
        Point S6Green = new Point(386, 405);
        Point S8red = new Point(413, 455);
        Point S8Green = new Point(406, 455);
        Point S5red = new Point(384, 150);
        Point S5Green = new Point(377, 150);
        Point SFred = new Point(980, 240);
        Point SFGreen = new Point(973, 240);
        Point Sred = new Point(980, 303);
        Point SGreen = new Point(973, 303);


        /// <summary>
        /// /////////////////////////////////多线程处理以及声明
        /// </summary>

        Thread snp_3 = null;
        Thread snp_8 = null;
        Thread snp_6 = null;
        Thread snp_5 = null;
        Thread snp_4 = null;
        Thread xnp_3 = null;
        Thread xnp_5 = null;
        Thread xnp_4 = null;
        Thread xnp_6 = null;
        Thread xnp_8 = null;
        Thread xp = null;
        Thread sp = null;
        public Field()
        {
            this.WindowState = FormWindowState.Normal;//bug fix

            this.Size = new Size(1300, 750);
            this.BackColor = Color.Black;
            back.Size = new Size(1300, 750);
            back.BackColor = Color.Black;
            front.Size = new Size(1300, 750);
            front.BackColor = Color.Transparent;
            this.Controls.Add(back);
            this.back.SendToBack();
            this.front.BringToFront();
            back.Visible = true;
            front.Visible = true;
            this.Show();
            DrawLine();
            trainTimer.Interval = 300;

            Font LineId = new Font("宋体", 8);
            NameThread();//生成线程
            Namesnake();//确定各动画起始点
            Train train1 = new Train() { arrTime = 123, depTime = 130, direction = 2, LineID = 3, trainID = "K21" };     
            Assign(train1);//按照列车的方向与股道筛选动画并执行
        }
        public void SnP_3(object train)
        {
            //主循环，控制动画
            while (true)
            {
                //每隔0.3秒更新一次
                Thread.Sleep(300);

                //程序主体
                Point temp = snake[1];
                DrawTrain(temp.X, temp.Y, Color.Red);
                DrawRedLight(S3red);

                if (count1 == 0)
                {
                    DrawTrain(temp.X, temp.Y, Color.Red);
                    count1++;
                }
                if ((s3count == 7) || (s3count > 7))
                {
                    DrawRedLight(Sred);
                    s3count++;
                }
                if (s3count < 7)
                {
                    DrawGreenLight(SGreen);
                    s3count++;
                }
                if (snake[1].X == 960)
                {
                    DrawRedLight(Sred);
                    TurnOff(SGreen);
                }
                if (snake[1].X > 795)
                {
                    snake[1].X = temp.X - 15;
                }
                if (snake[1].X == 795)
                {
                    snake[1].Y = temp.Y - 50;
                    snake[1].X = temp.X - 15;
                }
                if ((snake[1].X < 795) && (snake[1].X > 390))
                {
                    snake[1].X = temp.X - 15;
                }
                DrawTrain(snake[1].X, snake[1].Y, Color.Red);
                if (snake[1].X == 420)
                {
                    Thread.Sleep(1000 * (((Train)train).depTime - ((Train)train).arrTime));
                    DrawGreenLight(S3Green);
                    TurnOff(S3red);
                    while (true)
                    {
                        Thread.Sleep(300);
                        temp = snake[1];
                        DrawTrain(snake[1].X, snake[1].Y, Color.Red);
                        if (snake[1].X < 420)
                        {
                            snake[1].X = temp.X - 15;
                        }
                        if (snake[1].X == 390)
                        {
                            snake[1].X = temp.X - 15;
                        }
                        if ((snake[1].X < 390) || (snake[1].X > 195))
                        {

                            snake[1].X = temp.X - 15;
                        }
                        if (snake[1].X == 195)
                        {
                            snake[1].X = temp.X - 15;
                            snake[1].Y = temp.Y + 50;

                        }
                        if ((snake[1].X < 195) || (snake[1].X > 150))
                        {

                            snake[1].X = temp.X - 15;
                        }
                        if (snake[1].X == 150)
                        {
                            snake[1].X = temp.X - 15;
                            snake[1].Y = temp.Y + 50;

                        }
                        if (snake[1].X == 0)
                        {
                            break;
                        }

                    }
                }

                //再退一次
                if (snake[1].X == 0)
                {
                    DrawTrain(snake[1].X, snake[1].Y, Color.Red);
                    break;
                }

            }

        }
        public void SnP_5(object train)
        {
            //主循环，控制动画
            while (true)
            {
                //每隔0.3秒更新一次
                Thread.Sleep(300);

                //程序主体
                Point temp = snake[3];
                DrawTrain(temp.X, temp.Y, Color.Red);
                DrawRedLight(S5red);

                if (count3 == 0)
                {
                    DrawTrain(temp.X, temp.Y, Color.Red);
                    count3++;
                }
                if ((s5count == 7) || (s5count > 7))
                {
                    DrawRedLight(Sred);
                    s5count++;
                }
                if (s5count < 7)
                {
                    DrawGreenLight(SGreen);
                    s5count++;
                }
                if (snake[3].X == 960)
                {
                    DrawRedLight(Sred);
                    TurnOff(SGreen);
                }
                if (snake[3].X > 795)
                {
                    snake[3].X = temp.X - 15;
                }
                if (snake[3].X == 795)
                {
                    snake[3].Y = temp.Y - 50;
                    snake[3].X = temp.X - 15;
                }
                if (snake[3].X == 690)
                {
                    snake[3].Y = temp.Y - 50;
                    snake[3].X = temp.X - 15;
                }

                if ((snake[3].X < 795) && (snake[3].X > 390))
                {
                    snake[3].X = temp.X - 15;
                }
                DrawTrain(snake[3].X, snake[3].Y, Color.Red);
                if (snake[3].X == 420)
                {
                    Thread.Sleep(1000 * (((Train)train).depTime - ((Train)train).arrTime));
                    DrawGreenLight(S5Green);
                    TurnOff(S5red);
                    while (true)
                    {
                        Thread.Sleep(300);
                        temp = snake[3];
                        DrawTrain(snake[3].X, snake[3].Y, Color.Red);
                        if (snake[3].X == 390)
                        {
                            snake[3].X = temp.X - 15;
                        }
                        if ((snake[3].X < 390) || (snake[3].X > 315))
                        {
                            snake[3].X = temp.X - 15;
                        }
                        if (snake[3].X == 315)
                        {
                            snake[3].X = temp.X - 15;
                            snake[3].Y = temp.Y + 50;
                        }

                        if ((snake[3].X < 405) || (snake[3].X > 195))
                        {
                            snake[3].X = temp.X - 15;
                        }
                        if (snake[3].X == 195)
                        {
                            snake[3].X = temp.X - 15;
                            snake[3].Y = temp.Y + 50;
                        }
                        if ((snake[3].X < 195) || (snake[3].X > 150))
                        {
                            snake[3].X = temp.X - 15;
                        }
                        if (snake[3].X == 150)
                        {
                            snake[3].X = temp.X - 15;
                            snake[3].Y = temp.Y + 50;
                        }

                        DrawTrain(snake[3].X, snake[3].Y, Color.Red);
                        if ((snake[3].X == 415) || (snake[3].X < 415))
                        {
                            TurnOff(S5Green);
                            DrawRedLight(S5red);
                        }
                        if (snake[3].X == 0)
                        {
                            break;
                        }

                    }
                }

                //再退一次
                if (snake[3].X == 0)
                {
                    DrawTrain(snake[3].X, snake[3].Y, Color.Red);
                    break;
                }

            }

        }
        public void SnP_4(object train)
        {
            //主循环，控制动画
            while (true)
            {
                //每隔0.3秒更新一次
                Thread.Sleep(300);

                //程序主体
                Point temp = snake[4];
                DrawTrain(temp.X, temp.Y, Color.Red);
                DrawRedLight(S4red);

                if (count4 == 0)
                {
                    DrawTrain(temp.X, temp.Y, Color.Red);
                    count4++;
                }
                if ((s4count == 7) || (s4count > 7))
                {
                    DrawRedLight(Sred);
                    s4count++;
                }
                if (s4count < 7)
                {
                    DrawGreenLight(SGreen);
                    s4count++;
                }
                if (snake[4].X == 960)
                {
                    DrawRedLight(Sred);
                    TurnOff(SGreen);
                }
                if (snake[4].X > 750)
                {
                    snake[4].X = temp.X - 15;
                }
                if (snake[4].X == 750)
                {
                    snake[4].Y = temp.Y + 25;
                    snake[4].X = temp.X - 15;
                }
                if ((snake[4].X < 750) && (snake[4].X > 390))
                {
                    snake[4].X = temp.X - 15;
                }
                DrawTrain(snake[4].X, snake[4].Y, Color.Red);
                if (snake[4].X == 390)
                {
                    Thread.Sleep(1000 * (((Train)train).depTime - ((Train)train).arrTime));
                    DrawGreenLight(S4Green);
                    TurnOff(S4red);
                    while (true)
                    {
                        Thread.Sleep(300);
                        temp = snake[4];
                        DrawTrain(snake[4].X, snake[4].Y, Color.Red);
                        if (snake[4].X == 390)
                        {
                            snake[4].X = temp.X - 15;
                        }
                        if (snake[4].X < 390)
                        {
                            snake[4].X = temp.X - 15;
                        }
                        if (snake[4].X == 225)
                        {
                            snake[4].Y = temp.Y - 50;
                            snake[4].X = temp.X - 15;
                        }

                        DrawTrain(snake[4].X, snake[4].Y, Color.Red);
                        if ((snake[4].X == 415) || (snake[4].X < 415))
                        {
                            TurnOff(S4Green);
                            DrawRedLight(S4red);
                        }
                        if (snake[4].X == 0)
                        {
                            break;
                        }

                    }
                }

                //再退一次
                if (snake[4].X == 0)
                {
                    DrawTrain(snake[4].X, snake[4].Y, Color.Red);
                    break;
                }

            }

        }
        public void SnP_6(object train)
        {
            //主循环，控制动画
            while (true)
            {
                //每隔0.3秒更新一次
                Thread.Sleep(300);

                //程序主体
                Point temp = snake[2];
                DrawTrain(temp.X, temp.Y, Color.Red);
                DrawRedLight(S6red);

                if (count2 == 0)
                {
                    DrawTrain(temp.X, temp.Y, Color.Red);
                    count2++;
                }
                if ((s6count == 7) || (s6count > 7))
                {
                    DrawRedLight(Sred);
                    s6count++;
                }
                if (s6count < 7)
                {
                    DrawGreenLight(SGreen);
                    s6count++;
                }
                if (snake[2].X == 960)
                {
                    DrawRedLight(Sred);
                    TurnOff(SGreen);
                }
                if (snake[2].X > 750)
                {
                    snake[2].X = temp.X - 15;
                }
                if (snake[2].X == 750)
                {
                    snake[2].Y = temp.Y + 25;
                    snake[2].X = temp.X - 15;
                }
                if (snake[2].X == 675)
                {
                    snake[2].Y = temp.Y + 50;
                    snake[2].X = temp.X - 15;
                }
                if ((snake[2].X > 675) && (snake[2].X < 745))
                {
                    snake[2].X = temp.X - 15;
                }
                if ((snake[2].X < 675) && (snake[2].X > 390) && (snake[2].X != 390))
                {
                    snake[2].X = temp.X - 15;
                }
                DrawTrain(snake[2].X, snake[2].Y, Color.Red);
                if (snake[2].X == 420)
                {
                    Thread.Sleep(1000 * (((Train)train).depTime - ((Train)train).arrTime));
                    DrawGreenLight(S6Green);
                    TurnOff(S6red);
                    while (true)
                    {
                        Thread.Sleep(300);
                        temp = snake[2];
                        DrawTrain(snake[2].X, snake[2].Y, Color.Red);
                        if (snake[2].X == 420)
                        {
                            snake[2].X = temp.X - 15;
                        }
                        if (snake[2].X < 420)
                        {
                            snake[2].X = temp.X - 15;
                        }
                        if (snake[2].X == 330)
                        {
                            snake[2].Y = temp.Y - 50;
                            snake[2].X = temp.X - 15;
                        }
                        if (snake[2].X == 300)
                        {
                            snake[2].Y = temp.Y - 50;
                            snake[2].X = temp.X - 15;
                        }
                                       
                        DrawTrain(snake[2].X, snake[2].Y, Color.Red);
                        if ((snake[2].X == 415) || (snake[2].X < 415))
                        {
                            TurnOff(S6Green);
                            DrawRedLight(S6red);
                        }
                        if (snake[2].X == 0)
                        {
                            break;
                        }

                    }
                }

                //再退一次
                if (snake[2].X == 0)
                {
                    DrawTrain(snake[2].X, snake[2].Y, Color.Red);
                    break;
                }

            }

        }
        public void SnP_8(object train)
        {

            
            //主循环，控制动画
            while (true)
            {
                //每隔0.3秒更新一次
                Thread.Sleep(300);

                //程序主体
                Point temp = snake[0];
                DrawTrain(temp.X, temp.Y, Color.Red);
                DrawRedLight(S8red);

                if (count == 0)
                {
                    DrawTrain(temp.X, temp.Y, Color.Red);
                    count++;
                }
                if ((s8count == 7) || (s8count > 7))
                {
                    DrawRedLight(Sred);
                    s8count++;
                }
                if (s8count < 7)
                {
                    DrawGreenLight(SGreen);
                    s8count++;
                }
                if (snake[0].X == 960)
                {
                    DrawRedLight(Sred);
                    TurnOff(SGreen);
                }
                if (snake[0].X > 750)
                {
                    snake[0].X = temp.X - 15;
                }
                if (snake[0].X == 750)
                {
                    snake[0].Y = temp.Y + 25;
                    snake[0].X = temp.X - 15;
                }
                if (snake[0].X == 675)
                {
                    snake[0].Y = temp.Y + 50;
                    snake[0].X = temp.X - 15;
                }
                if (snake[0].X == 615)
                {
                    snake[0].Y = temp.Y + 50;
                    snake[0].X = temp.X - 15;
                }

                if ((snake[0].X > 615) && (snake[0].X < 745))
                {
                    snake[0].X = temp.X - 15;
                }
                if ((snake[0].X < 675) && (snake[0].X > 420) && (snake[0].X != 420))
                {
                    snake[0].X = temp.X - 15;
                }
                DrawTrain(snake[0].X, snake[0].Y, Color.Red);
                if (snake[0].X == 420)
                {
                    Thread.Sleep(1000*(((Train)train).depTime- ((Train)train).arrTime));
                    DrawGreenLight(S8Green);
                    TurnOff(S8red);
                    while (true)
                    {
                        Thread.Sleep(300);
                        temp = snake[0];
                        DrawTrain(snake[0].X, snake[0].Y, Color.Red);
                        if (snake[0].X == 420)
                        {
                            snake[0].X = temp.X - 15;
                        }
                        if (snake[0].X < 420)
                        {
                            snake[0].X = temp.X - 15;
                        }
                        if (snake[0].X == 330)
                        {
                            snake[0].Y = temp.Y - 50;
                            snake[0].X = temp.X - 15;
                        }
                        if (snake[0].X == 300)
                        {
                            snake[0].Y = temp.Y - 50;
                            snake[0].X = temp.X - 15;
                        }
                        if (snake[0].X == 195)
                        {
                            snake[0].Y = temp.Y - 50;
                            snake[0].X = temp.X - 15;
                        }
                        DrawTrain(snake[0].X, snake[0].Y, Color.Red);
                        if ((snake[0].X == 415)||(snake[0].X< 415))
                        {
                            TurnOff(S8Green);
                            DrawRedLight(S8red);
                        }
                            if (snake[0].X == 0)
                        {
                            break;
                        }

                    }
                }

                //再退一次
                if (snake[0].X == 0)
                {
                    DrawTrain(snake[0].X, snake[0].Y, Color.Red);
                    break;
                }

            }

        }
        public void Xnp_3(object train)
        {

            //主循环，控制动画
            while (true)
            {
                //每隔0.3秒更新一次
                Thread.Sleep(300);

                //程序主体
                Point temp = snake[5];
                DrawTrain(temp.X, temp.Y, Color.Red);
                DrawRedLight(X3red);

                if (count5 == 0)
                {
                    DrawTrain(temp.X, temp.Y, Color.Red);
                    count5++;
                }
                if ((x3count == 2) || (x3count > 2))
                {
                    DrawRedLight(Xred);
                    x3count++;
                }
                if (x3count < 2)
                {
                    DrawGreenLight(XGreen);
                    x3count++;
                }
                if (snake[5].X == 15)
                {
                    DrawRedLight(Xred);
                    TurnOff(XGreen);
                }
                if (snake[5].X < 195)
                {
                    snake[5].X = temp.X + 15;
                }
                if (snake[5].X == 195)
                {
                    snake[5].Y = temp.Y - 25;
                    snake[5].X = temp.X + 15;
                }
                if ((snake[5].X > 195) && (snake[5].X < 555) && (snake[5].X != 555))
                {
                    snake[5].X = temp.X + 15;
                }
                DrawTrain(snake[5].X, snake[5].Y, Color.Red);
                if (snake[5].X == 555)
                {
                    Thread.Sleep(1000 * (((Train)train).depTime - ((Train)train).arrTime));
                    DrawGreenLight(X3Green);
                    TurnOff(X3red);
                    while (true)
                    {
                        Thread.Sleep(300);
                        temp = snake[5];
                        DrawTrain(snake[5].X, snake[5].Y, Color.Red);
                        if (((snake[5].X > 555) || (snake[5].X == 555)) && (snake[5].X < 750))
                        {
                            snake[5].X = temp.X + 15;
                        }
                        if (snake[5].X == 750)
                        {
                            snake[5].Y = temp.Y + 25;
                            snake[5].X = temp.X + 15;
                        }
                        if (snake[5].X > 750)
                        {
                            snake[5].X = temp.X + 15;
                        }
                        DrawTrain(snake[5].X, snake[5].Y, Color.Red);
                        if ((snake[5].X == 415) || (snake[5].X < 415))
                        {
                            TurnOff(X3Green);
                            DrawRedLight(X3red);
                        }
                        if (snake[5].X == 0)
                        {
                            break;
                        }

                    }
                }

                //再退一次
                if (snake[5].X == 0)
                {
                    DrawTrain(snake[5].X, snake[5].Y, Color.Red);
                    break;
                }

            }

        }
        public void Xnp_5(object train)
        {

            //主循环，控制动画
            while (true)
            {
                //每隔0.3秒更新一次
                Thread.Sleep(300);

                //程序主体
                Point temp = snake[6];
                DrawTrain(temp.X, temp.Y, Color.Red);
                DrawRedLight(X5red);

                if (count7 == 0)
                {
                    DrawTrain(temp.X, temp.Y, Color.Red);
                    count7++;
                }
                if ((x5count == 2) || (x5count > 2))
                {
                    DrawRedLight(Xred);
                    x5count++;
                }
                if (x5count < 2)
                {
                    DrawGreenLight(XGreen);
                    x5count++;
                }
                if (snake[6].X < 195)
                {
                    snake[6].X = temp.X + 15;
                }
                if (snake[6].X == 30)
                {
                    DrawRedLight(Xred);
                    TurnOff(XGreen);
                }               
                if (snake[6].X == 195)
                {
                    snake[6].Y = temp.Y - 25;
                    snake[6].X = temp.X + 15;
                }
                if (snake[6].X == 285)
                {
                    snake[6].Y = temp.Y - 50;
                    snake[6].X = temp.X + 15;
                }
                if ((snake[6].X > 195) && (snake[6].X < 555) && (snake[6].X != 555))
                {
                    snake[6].X = temp.X + 15;
                }
                DrawTrain(snake[6].X, snake[6].Y, Color.Red);
                if (snake[6].X == 555)
                {
                    Thread.Sleep(1000 * (((Train)train).depTime - ((Train)train).arrTime));
                    DrawGreenLight(X5Green);
                    TurnOff(X5red);
                    while (true)
                    {
                        Thread.Sleep(300);
                        temp = snake[6];
                        DrawTrain(snake[6].X, snake[6].Y, Color.Red);
                        if (((snake[6].X > 555) || (snake[6].X == 555)) && (snake[6].X < 750))
                        {
                            snake[6].X = temp.X + 15;
                        }
                        if (snake[6].X == 645)
                        {
                            snake[6].Y = temp.Y + 50;
                            snake[6].X = temp.X + 15;
                        }

                        if (snake[6].X == 750)
                        {
                            snake[6].Y = temp.Y + 25;
                            snake[6].X = temp.X + 15;
                        }
                        if (snake[6].X > 750)
                        {
                            snake[6].X = temp.X + 15;
                        }
                        DrawTrain(snake[6].X, snake[6].Y, Color.Red);
                        if ((snake[6].X == 415) || (snake[6].X < 415))
                        {
                            TurnOff(X5Green);
                            DrawRedLight(X5red);
                        }
                        if (snake[6].X == 0)
                        {
                            break;
                        }

                    }
                }

                //再退一次
                if (snake[6].X == 0)
                {
                    DrawTrain(snake[6].X, snake[6].Y, Color.Red);
                    break;
                }

            }

        }
        public void Xnp_4(object train)
        {

            //主循环，控制动画
            while (true)
            {
                //每隔0.3秒更新一次
                Thread.Sleep(300);

                //程序主体
                Point temp = snake[7];
                DrawTrain(temp.X, temp.Y, Color.Red);
                DrawRedLight(X4red);

                if (count6 == 0)
                {
                    DrawTrain(temp.X, temp.Y, Color.Red);
                    count6++;
                }
                if ((x4count == 2) || (x4count > 2))
                {
                    DrawRedLight(Xred);
                    x4count++;
                }
                if (x4count < 2)
                {
                    DrawGreenLight(XGreen);
                    x4count++;
                }
                if (snake[7].X < 195)
                {
                    snake[7].X = temp.X + 15;
                }
                if (snake[7].X == 30)
                {
                    DrawRedLight(Xred);
                    TurnOff(XGreen);
                }
                if (snake[7].X == 45)
                {
                    snake[7].Y = temp.Y + 50;
                    snake[7].X = temp.X + 15;
                }

                if (snake[7].X == 195)
                {
                    snake[7].Y = temp.Y + 25;
                    snake[7].X = temp.X + 15;
                }
                if ((snake[7].X > 195) && (snake[7].X < 600) && (snake[7].X != 600))
                {
                    snake[7].X = temp.X + 15;
                }
                DrawTrain(snake[7].X, snake[7].Y, Color.Red);
                if (snake[7].X == 600)
                {
                    Thread.Sleep(1000 * (((Train)train).depTime - ((Train)train).arrTime));
                    DrawGreenLight(X4Green);
                    TurnOff(X4red);
                    while (true)
                    {
                        Thread.Sleep(300);
                        temp = snake[7];
                        DrawTrain(snake[7].X, snake[7].Y, Color.Red);
                        if (((snake[7].X > 600) || (snake[7].X == 600)) && (snake[7].X < 750))
                        {
                            snake[7].X = temp.X + 15;
                        }
                        if (snake[7].X == 750)
                        {
                            snake[7].Y = temp.Y - 25;
                            snake[7].X = temp.X + 15;
                        }
                        if ((snake[7].X > 750) && (snake[7].X < 900))
                        {
                            snake[7].X = temp.X + 15;
                        }
                        if (snake[7].X == 900)
                        {
                            snake[7].Y = temp.Y - 25;
                            snake[7].X = temp.X + 15;
                        }

                        if (snake[7].X > 900)
                        {
                            snake[7].X = temp.X + 15;
                        }
                        DrawTrain(snake[7].X, snake[7].Y, Color.Red);
                        if ((snake[7].X == 615) || (snake[7].X < 615))
                        {
                            TurnOff(X4Green);
                            DrawRedLight(X4red);
                        }
                        if (snake[7].X == 0)
                        {
                            break;
                        }

                    }
                }

                //再退一次
                if (snake[7].X == 0)
                {
                    DrawTrain(snake[7].X, snake[7].Y, Color.Red);
                    break;
                }

            }

        }
        public void Xnp_6(object train)
        {

            //主循环，控制动画
            while (true)
            {
                //每隔0.3秒更新一次
                Thread.Sleep(300);

                //程序主体
                Point temp = snake[8];
                DrawTrain(temp.X, temp.Y, Color.Red);
                DrawRedLight(X6red);

                if (count8 == 0)
                {
                    DrawTrain(temp.X, temp.Y, Color.Red);
                    count8++;
                }
                if ((x6count == 2) || (x6count > 2))
                {
                    DrawRedLight(Xred);
                    x6count++;
                }
                if (x6count < 2)
                {
                    DrawGreenLight(XGreen);
                    x6count++;
                }
                if (snake[8].X < 195)
                {
                    snake[8].X = temp.X + 15;
                }
                if (snake[8].X == 30)
                {
                    DrawRedLight(Xred);
                    TurnOff(XGreen);
                }
                if (snake[8].X == 45)
                {
                    snake[8].Y = temp.Y + 50;
                    snake[8].X = temp.X + 15;
                }

                if (snake[8].X == 195)
                {
                    snake[8].Y = temp.Y + 25;
                    snake[8].X = temp.X + 15;
                }
                if (snake[8].X == 240)
                {
                    snake[8].Y = temp.Y + 45;
                    snake[8].X = temp.X + 15;
                }

                if ((snake[8].X > 195) && (snake[8].X < 540) && (snake[8].X != 540))
                {
                    snake[8].X = temp.X + 15;
                }
                DrawTrain(snake[8].X, snake[8].Y, Color.Red);
                if (snake[8].X == 540)
                {
                    Thread.Sleep(1000 * (((Train)train).depTime - ((Train)train).arrTime));
                    DrawGreenLight(X6Green);
                    TurnOff(X6red);
                    while (true)
                    {
                        Thread.Sleep(300);
                        temp = snake[8];
                        DrawTrain(snake[8].X, snake[8].Y, Color.Red);
                        if (snake[8].X == 630)
                        {
                            snake[8].Y = temp.Y - 50;
                            snake[8].X = temp.X + 15;
                        }
                        if (((snake[8].X > 540) || (snake[8].X == 540)) && (snake[8].X < 750))
                        {
                            snake[8].X = temp.X + 15;
                        }
                        if (snake[8].X == 750)
                        {
                            snake[8].Y = temp.Y - 25;
                            snake[8].X = temp.X + 15;
                        }
                        if ((snake[8].X > 750) && (snake[8].X < 900))
                        {
                            snake[8].X = temp.X + 15;
                        }
                        if (snake[8].X == 900)
                        {
                            snake[8].Y = temp.Y - 25;
                            snake[8].X = temp.X + 15;
                        }

                        if (snake[8].X > 900)
                        {
                            snake[8].X = temp.X + 15;
                        }
                        DrawTrain(snake[8].X, snake[8].Y, Color.Red);
                        if ((snake[8].X == 570) || (snake[8].X < 570))
                        {
                            TurnOff(X6Green);
                            DrawRedLight(X6red);
                        }
                        if (snake[8].X == 0)
                        {
                            break;
                        }

                    }
                }

                //再退一次
                if (snake[8].X == 0)
                {
                    DrawTrain(snake[8].X, snake[8].Y, Color.Red);
                    break;
                }

            }

        }
        public void Xnp_8(object train)
        {

            //主循环，控制动画
            while (true)
            {
                //每隔0.3秒更新一次
                Thread.Sleep(300);

                //程序主体
                Point temp = snake[9];
                DrawTrain(temp.X, temp.Y, Color.Red);
                DrawRedLight(X8red);

                if (count9 == 0)
                {
                    DrawTrain(temp.X, temp.Y, Color.Red);
                    count9++;
                }
                if ((x8count == 2) || (x8count > 2))
                {
                    DrawRedLight(Xred);
                    x8count++;
                }
                if (x8count < 2)
                {
                    DrawGreenLight(XGreen);
                    x8count++;
                }
                if (snake[9].X < 195)
                {
                    snake[9].X = temp.X + 15;
                }
                if (snake[9].X == 30)
                {
                    DrawRedLight(Xred);
                    TurnOff(XGreen);
                }
                if (snake[9].X == 45)
                {
                    snake[9].Y = temp.Y + 50;
                    snake[9].X = temp.X + 15;
                }

                if (snake[9].X == 195)
                {
                    snake[9].Y = temp.Y + 25;
                    snake[9].X = temp.X + 15;
                }
                if (snake[9].X == 240)
                {
                    snake[9].Y = temp.Y + 50;
                    snake[9].X = temp.X + 15;
                }
                if (snake[9].X == 345)
                {
                    snake[9].Y = temp.Y + 50;
                    snake[9].X = temp.X + 15;
                }
                if ((snake[9].X > 195) && (snake[9].X < 510) && (snake[9].X != 510))
                {
                    snake[9].X = temp.X + 15;
                }
                DrawTrain(snake[9].X, snake[9].Y, Color.Red);
                if (snake[9].X == 510)
                {
                    Thread.Sleep(1000 * (((Train)train).depTime - ((Train)train).arrTime));
                    DrawGreenLight(X8Green);
                    TurnOff(X8red);
                    while (true)
                    {
                        Thread.Sleep(300);
                        temp = snake[9];
                        DrawTrain(snake[9].X, snake[9].Y, Color.Red);
                        if (snake[9].X == 600)
                        {
                            snake[9].Y = temp.Y - 50;
                            snake[9].X = temp.X + 15;
                        }

                        if (snake[9].X == 630)
                        {
                            snake[9].Y = temp.Y - 50;
                            snake[9].X = temp.X + 15;
                        }
                        if (((snake[9].X > 510) || (snake[9].X == 510)) && (snake[9].X < 750))
                        {
                            snake[9].X = temp.X + 15;
                        }
                        if (snake[9].X == 750)
                        {
                            snake[9].Y = temp.Y - 25;
                            snake[9].X = temp.X + 15;
                        }
                        if ((snake[9].X > 750) && (snake[9].X < 900))
                        {
                            snake[9].X = temp.X + 15;
                        }
                        if (snake[9].X == 900)
                        {
                            snake[9].Y = temp.Y - 25;
                            snake[9].X = temp.X + 15;
                        }

                        if (snake[9].X > 900)
                        {
                            snake[9].X = temp.X + 15;
                        }
                        DrawTrain(snake[9].X, snake[9].Y, Color.Red);
                        if ((snake[9].X == 570) || (snake[9].X < 570))
                        {
                            TurnOff(X8Green);
                            DrawRedLight(X8red);
                        }
                        if (snake[9].X == 0)
                        {
                            break;
                        }

                    }
                }

                //再退一次
                if (snake[9].X == 0)
                {
                    DrawTrain(snake[9].X, snake[9].Y, Color.Red);
                    break;
                }

            }

        }
        public void Xp(object train)
        {

            //主循环，控制动画
            while (true)
            {
                //每隔0.3秒更新一次
                Thread.Sleep(300);

                //程序主体
                Point temp = snake[10];
                DrawTrain(temp.X, temp.Y, Color.Red);
                TurnOff(Xred);
               
                if (count10 == 0)
                {
                    DrawTrain(temp.X, temp.Y, Color.Red);
                    count10++;
                }
                if ((xcount == 2) || (xcount > 2))
                {
                    DrawRedLight(Xred);
                    xcount++;
                }
                if (xcount < 2)
                {
                    DrawGreenLight(XGreen);
                    xcount++;
                }
                if (snake[10].X == 30)
                {
                    DrawRedLight(Xred);
                    TurnOff(XGreen);
                }
                if (snake[10].X < 1200)
                {
                    snake[10].X = temp.X + 15;
                }
                
                DrawTrain(snake[10].X, snake[10].Y, Color.Red);
                

                //再退一次
                if (snake[10].X == 1200)
                {
                    DrawTrain(snake[10].X, snake[10].Y, Color.Red);
                    break;
                }

            }

        }
        public void Sp(object train)
        {

            //主循环，控制动画
            while (true)
            {
                //每隔0.3秒更新一次
                Thread.Sleep(300);

                //程序主体
                Point temp = snake[11];
                DrawTrain(temp.X, temp.Y, Color.Red);
                TurnOff(Sred);

                if (count11 == 0)
                {
                    DrawTrain(temp.X, temp.Y, Color.Red);
                    count11++;
                }
                if ((scount == 7) || (scount > 7))
                {
                    DrawRedLight(Sred);
                    scount++;
                }
                if (scount < 7)
                {
                    DrawGreenLight(SGreen);
                    scount++;
                }             
                if (snake[11].X == 960)
                {
                    DrawRedLight(Sred);
                    TurnOff(SGreen);
                }
                if (snake[11].X < 1200)
                {
                    snake[11].X = temp.X - 15;
                }

                DrawTrain(snake[11].X, snake[11].Y, Color.Red);


                //再退一次
                if (snake[11].X == 0)
                {
                    DrawTrain(snake[11].X, snake[11].Y, Color.Red);
                    break;
                }

            }

        }
        public void DrawTrain(int x, int y,System.Drawing.Color color)
        {
            try
            {
                Graphics grfx = back.CreateGraphics();
                System.IntPtr hdc = grfx.GetHdc();

                //interop and good old GDI

                System.IntPtr hpen =
                CreatePen(0, 5, System.Drawing.ColorTranslator.ToWin32(color));

                int rop = SetROP2(hdc, 7);

                IntPtr oldpen = SelectObject(hdc, hpen);

                MoveToEx(hdc, x, y, IntPtr.Zero);

                LineTo(hdc, x + 40, y);

                SelectObject(hdc, oldpen);

                SetROP2(hdc, rop);

                grfx.ReleaseHdc(hdc);
            }
            catch (Exception e)
            {
                snp_3.Abort();
            }
            

        }
        public void DrawLine()
        {
            Graphics g = back.CreateGraphics();
            Font LineId = new Font("宋体", 8);
            g.DrawString(dt.ToString(), LineId, Brushes.White, (float)0, (float)0);

            //股道
            g.DrawLine(new Pen(Color.White), 350, 150, 650, 150);
            g.DrawLine(new Pen(Color.White), 200, 200, 1100, 200);
            g.DrawLine(new Pen(Color.White), 30, 250, 1240, 250);
            g.DrawLine(new Pen(Color.White), 30, 300, 1240, 300);
            g.DrawLine(new Pen(Color.White), 200, 350, 1000, 350);
            g.DrawLine(new Pen(Color.White), 330, 400, 650, 400);
            g.DrawLine(new Pen(Color.White), 200, 450, 750, 450);
            //车挡
            g.DrawLine(new Pen(Color.White), 200, 195, 200, 205);
            g.DrawLine(new Pen(Color.White), 195, 195, 200, 195);
            g.DrawLine(new Pen(Color.White), 195, 205, 200, 205);

            g.DrawLine(new Pen(Color.White), 200, 345, 200, 355);
            g.DrawLine(new Pen(Color.White), 195, 345, 200, 345);
            g.DrawLine(new Pen(Color.White), 195, 355, 200, 355);

            g.DrawLine(new Pen(Color.White), 200, 445, 200, 455);
            g.DrawLine(new Pen(Color.White), 195, 445, 200, 445);
            g.DrawLine(new Pen(Color.White), 195, 455, 200, 455);

            g.DrawLine(new Pen(Color.White), 1100, 195, 1100, 205);
            g.DrawLine(new Pen(Color.White), 1100, 195, 1105, 195);
            g.DrawLine(new Pen(Color.White), 1100, 205, 1105, 205);

            g.DrawLine(new Pen(Color.White), 1000, 345, 1000, 355);
            g.DrawLine(new Pen(Color.White), 1000, 345, 1005, 345);
            g.DrawLine(new Pen(Color.White), 1000, 355, 1005, 355);

            //道岔
            g.DrawLine(new Pen(Color.White), 60, 250, 100, 300);
            g.DrawLine(new Pen(Color.White), 120, 300, 170, 250);
            g.DrawLine(new Pen(Color.White), 200, 250, 250, 200);
            g.DrawLine(new Pen(Color.White), 300, 200, 350, 150);
            g.DrawLine(new Pen(Color.White), 650, 150, 700, 200);
            g.DrawLine(new Pen(Color.White), 200, 300, 250, 350);
            g.DrawLine(new Pen(Color.White), 280, 350, 330, 400);
            g.DrawLine(new Pen(Color.White), 650, 400, 700, 350);
            g.DrawLine(new Pen(Color.White), 350, 400, 400, 450);
            g.DrawLine(new Pen(Color.White), 590, 450, 640, 400);
            g.DrawLine(new Pen(Color.White), 650, 150, 700, 200);
            g.DrawLine(new Pen(Color.White), 820, 250, 870, 300);
            g.DrawLine(new Pen(Color.White), 950, 250, 900, 300);
            g.DrawLine(new Pen(Color.White), 750, 200, 800, 250);
            g.DrawLine(new Pen(Color.White), 750, 350, 800, 300);
            g.DrawLine(new Pen(Color.White), 750, 450, 850, 350);
            g.DrawLine(new Pen(Color.White), 675, 175, 800, 175);
            g.DrawLine(new Pen(Color.White), 800, 175, 825, 200);
            //股道标号
            g.DrawString("5道", LineId, Brushes.White, (float)470, (float)155);
            g.DrawString("3道", LineId, Brushes.White, (float)470, (float)205);
            g.DrawString("Ⅰ道", LineId, Brushes.White, (float)470, (float)255);
            g.DrawString("Ⅱ道", LineId, Brushes.White, (float)470, (float)305);
            g.DrawString("4道", LineId, Brushes.White, (float)470, (float)355);
            g.DrawString("6道", LineId, Brushes.White, (float)470, (float)405);
            g.DrawString("8道", LineId, Brushes.White, (float)470, (float)455);
            //道岔编号
            g.DrawString("1", LineId, Brushes.White, (float)50, (float)253);
            g.DrawString("3", LineId, Brushes.White, (float)97, (float)303);
            g.DrawString("5", LineId, Brushes.White, (float)117, (float)303);
            g.DrawString("7", LineId, Brushes.White, (float)135, (float)253);
            g.DrawString("9", LineId, Brushes.White, (float)195, (float)237);
            g.DrawString("11", LineId, Brushes.White, (float)233, (float)190);
            g.DrawString("13", LineId, Brushes.White, (float)193, (float)305);
            g.DrawString("15", LineId, Brushes.White, (float)243, (float)355);
            g.DrawString("17", LineId, Brushes.White, (float)268, (float)355);
            g.DrawString("19", LineId, Brushes.White, (float)338, (float)405);
            g.DrawString("21", LineId, Brushes.White, (float)388, (float)455);
            g.DrawString("23", LineId, Brushes.White, (float)289, (float)190);
            g.DrawString("2", LineId, Brushes.White, (float)935, (float)240);
            g.DrawString("4", LineId, Brushes.White, (float)895, (float)300);
            g.DrawString("6", LineId, Brushes.White, (float)860, (float)300);
            g.DrawString("8", LineId, Brushes.White, (float)820, (float)240);
            g.DrawString("10", LineId, Brushes.White, (float)785, (float)255);
            g.DrawString("12", LineId, Brushes.White, (float)745, (float)190);
            g.DrawString("14", LineId, Brushes.White, (float)785, (float)285);
            g.DrawString("16", LineId, Brushes.White, (float)745, (float)355);
            g.DrawString("18", LineId, Brushes.White, (float)695, (float)340);
            g.DrawString("20", LineId, Brushes.White, (float)640, (float)385);
            g.DrawString("22", LineId, Brushes.White, (float)585, (float)435);
            g.DrawString("24", LineId, Brushes.White, (float)835, (float)340);
            g.DrawString("26", LineId, Brushes.White, (float)820, (float)190);
            g.DrawString("28", LineId, Brushes.White, (float)695, (float)190);
            g.DrawString("30", LineId, Brushes.White, (float)670, (float)160);

            g.DrawString("郑西高铁上行", LineId, Brushes.Orange, (float)100, (float)370);
            g.DrawLine(new Pen(Color.Orange), 100, 360, 170, 360);
            g.DrawLine(new Pen(Color.Orange), 100, 360, 110, 370);

            g.DrawString("郑西高铁下行", LineId, Brushes.Orange, (float)1150, (float)210);
            g.DrawLine(new Pen(Color.Orange), 1150, 230, 1220, 230);
            g.DrawLine(new Pen(Color.Orange), 1220, 230, 1210, 220);
            //信号灯及其编号
            g.DrawString("X", LineId, Brushes.White, (float)30, (float)240);


            g.DrawString("XF", LineId, Brushes.White, (float)30, (float)300);


            g.DrawString("X1", LineId, Brushes.White, (float)735, (float)240);

            g.DrawString("X2", LineId, Brushes.White, (float)735, (float)290);


            g.DrawString("X3", LineId, Brushes.White, (float)595, (float)190);


            g.DrawString("X4", LineId, Brushes.White, (float)645, (float)340);


            g.DrawString("X6", LineId, Brushes.White, (float)585, (float)390);


            g.DrawString("X8", LineId, Brushes.White, (float)540, (float)440);


            g.DrawString("X5", LineId, Brushes.White, (float)595, (float)140);


            g.DrawString("S1", LineId, Brushes.White, (float)238, (float)250);


            g.DrawString("S2", LineId, Brushes.White, (float)238, (float)300);


            g.DrawString("S3", LineId, Brushes.White, (float)389, (float)200);


            g.DrawString("S4", LineId, Brushes.White, (float)318, (float)355);

            g.DrawString("S6", LineId, Brushes.White, (float)398, (float)405);


            g.DrawString("S8", LineId, Brushes.White, (float)418, (float)455);

            g.DrawString("S5", LineId, Brushes.White, (float)389, (float)150);


            g.DrawString("SF", LineId, Brushes.White, (float)985, (float)240);


            g.DrawString("S", LineId, Brushes.White, (float)985, (float)303);

        }
        public void DrawRedLight(Point point)
        {
            try
            {
                Graphics g = back.CreateGraphics();
                g.FillEllipse(Brushes.Red, point.X, point.Y, 5, 5);
            }
            catch (Exception)
            {
                
               
            }
           
        }
        public void DrawGreenLight(Point point)
        {
            try
            {
                Graphics g = back.CreateGraphics();
                g.FillEllipse(Brushes.Green, point.X, point.Y, 5, 5);
            }
            catch (Exception)
            {

              
            }
           
        }
        public void TurnOff(Point point)
        {
            try
            {
                Graphics g = back.CreateGraphics();
                g.FillEllipse(Brushes.Black, point.X, point.Y, 5, 5);
            }
            catch (Exception)
            {

                
            }
           
        }
        public void Assign(object train)
        {
            //direction:1为下行（向左） 2为上行（向右）
            if ((((Train)train).direction == 1)&& (((Train)train).LineID == 1))
            {
                xp.Start(train);
            }            
            if ((((Train)train).direction == 1) && (((Train)train).LineID == 3))
            {
                xnp_3.Start(train);
            }
            if ((((Train)train).direction == 1) && (((Train)train).LineID == 5))
            {
                xnp_5.Start(train);
            }
            if ((((Train)train).direction == 1) && (((Train)train).LineID == 4))
            {
                xnp_4.Start(train);
            }
            if ((((Train)train).direction == 1) && (((Train)train).LineID == 6))
            {
                xnp_6.Start(train);
            }
            if ((((Train)train).direction == 1) && (((Train)train).LineID == 8))
            {
                xnp_8.Start(train);
            }
            if ((((Train)train).direction == 2) && (((Train)train).LineID == 2))
            {
                sp.Start(train);
            }
            if ((((Train)train).direction == 2) && (((Train)train).LineID == 3))
            {
                snp_3.Start(train);
            }
            if ((((Train)train).direction == 2) && (((Train)train).LineID == 4))
            {
                snp_4.Start(train);
            }
            if ((((Train)train).direction == 2) && (((Train)train).LineID == 5))
            {
                snp_5.Start(train);
            }
            if ((((Train)train).direction == 2) && (((Train)train).LineID == 6))
            {
                snp_6.Start(train);
            }
            if ((((Train)train).direction == 2) && (((Train)train).LineID == 8))
            {
                snp_8.Start(train);
            }


        }//根据列车选择动画的方法
        public void Namesnake()
        {
            //Snp_8
            snake[0].X = 1050;
            snake[0].Y = 300;

            trainTimer.Start();

            //Snp_3
            snake[1].X = 1050;
            snake[1].Y = 300;

            //Snp_6
            snake[2].X = 1050;
            snake[2].Y = 300;

            //Snp_5
            snake[3].X = 1050;
            snake[3].Y = 300;

            //Snp_4
            snake[4].X = 1050;
            snake[4].Y = 300;

            //Xnp_3
            snake[5].X = 0;
            snake[5].Y = 250;

            //Xnp_5
            snake[6].X = 0;
            snake[6].Y = 250;

            //Xnp_4
            snake[7].X = 0;
            snake[7].Y = 250;

            //Xnp_6
            snake[8].X = 0;
            snake[8].Y = 250;

            //Xnp_8
            snake[9].X = 0;
            snake[9].Y = 250;

            //Xp
            snake[10].X = 0;
            snake[10].Y = 250;

            //Sp
            snake[11].X = 1050;
            snake[11].Y = 300;

        }
        public void NameThread()
        {
            snp_3 = new Thread(SnP_3);
            snp_5 = new Thread(SnP_5);
            snp_8 = new Thread(SnP_8);
            snp_6 = new Thread(SnP_6);
            snp_4 = new Thread(SnP_4);
            xnp_3 = new Thread(Xnp_3);
            xnp_5 = new Thread(Xnp_5);
            xnp_4 = new Thread(Xnp_4);
            xnp_6 = new Thread(Xnp_6);
            xnp_8 = new Thread(Xnp_8);
            xp = new Thread(Xp);
            sp = new Thread(Sp);
        }
    }
    public class Train
    {
        public string trainID { get; set; }
        public int direction { get; set; }
        public int LineID { get; set; }
        public int arrTime { get; set; }
        public int depTime { get; set; }
    }
    
}
