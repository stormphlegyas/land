using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

class Player
{
    static int[]    srf;
    static int[]    srfx;
    static int      X;
    static int      Y;
    static int      dest;
    static int findDest(int[] sf)
    {
        int i;
        for(i = 0; i < sf.Length - 1; i++)
        {
            if (sf[i] == sf[i + 1])
                break;
        }
        
        return (i);
    }
    static double sqr(double nb){
        return(nb * nb);
    }
    static int fallspeed(int vSpeed, int hSpeed, int power)
    {
        if (obstacle2(hSpeed, vSpeed) == true)
            return(4);
        if ((hSpeed < 0 && srfx[dest] > X) || (hSpeed > 0 && srfx[dest + 1] < X))
            return (4);
        int pwr = 0;
        double  cst = 0;
        if (power == 0)
            cst = 3 * 3.711 - 6;
        else if (power == 1)
            cst = 2 * 3.711 - 5;
        else if (power == 2)
            cst = 1 * 3.711 - 3;
        pwr = - (vSpeed - 4 - (int)Math.Round(cst)) - 20;
        
            if (pwr > 4)
                pwr = 4;
            if (pwr < 0)
                pwr = 0;
        return (pwr);
    }
    static bool obstacle(int hSpeed, int vSpeed){
        double Xa = X;
        double Ya = Y;
        double Xb = X + hSpeed;
        double Yb = Y + vSpeed;
        double Xc, Yc, Xd, Yd, Xi, Yi;
        double dist = 0;
        for(int i = 0; i < srf.Length - 1; i++){
            Xc = srfx[i];
            Yc = srf[i];
            Xd = srfx[i + 1];
            Yd = srf[i + 1];
            Xi = (((Yb-Ya)/(Xb-Xa))*Xa+Ya-((Yd-Yc)/(Xd-Xc))*Xc-Yc) / (((Yb-Ya)/(Xb-Xa))-((Yd-Yc)/(Xd-Xc)));
            Yi = ((Yb-Ya)/(Xb-Xa))*((((Yb-Ya)/(Xb-Xa))*Xa+Ya-((Yd-Yc)/(Xd-Xc))*Xc-Yc) / (((Yb-Ya)/(Xb-Xa))-((Yd-Yc)/(Xd-Xc)))-Xa)+Ya;
            dist = Math.Sqrt(sqr(Xi - X) + sqr(Yi - Ya));
        }
        Xc = srfx[dest];
        Yc = srf[dest];
        Xd = srfx[dest + 1];
        Yd = srf[dest + 1];
        Xi = (((Yb-Ya)/(Xb-Xa))*Xa+Ya-((Yd-Yc)/(Xd-Xc))*Xc-Yc) / (((Yb-Ya)/(Xb-Xa))-((Yd-Yc)/(Xd-Xc)));
        Yi = ((Yb-Ya)/(Xb-Xa))*((((Yb-Ya)/(Xb-Xa))*Xa+Ya-((Yd-Yc)/(Xd-Xc))*Xc-Yc) / (((Yb-Ya)/(Xb-Xa))-((Yd-Yc)/(Xd-Xc)))-Xa)+Ya;
        if (dist < Math.Sqrt(sqr(Xi - X) + sqr(Yi - Ya)))
            return(true);
        return(false);
    }
     static bool obstacle2(int hSpeed, int vSpeed){
        int target = srfx[dest]+ ((srfx[dest + 1] - srfx[dest]) / 2);
        double Xa = X;
        double Ya = Y;
        double Xb = X + (target - X);
        double Yb = Y + (srf[dest] - Y);
        double Xc, Yc, Xd, Yd, Xi, Yi;
        double dist = 0;
        for(int i = 0; i < srf.Length - 1; i++){
            Xc = srfx[i];
            Yc = srf[i];
            Xd = srfx[i + 1];
            Yd = srf[i + 1];
            Xi = (((Yb-Ya)/(Xb-Xa))*Xa+Ya-((Yd-Yc)/(Xd-Xc))*Xc-Yc) / (((Yb-Ya)/(Xb-Xa))-((Yd-Yc)/(Xd-Xc)));
            Yi = ((Yb-Ya)/(Xb-Xa))*((((Yb-Ya)/(Xb-Xa))*Xa+Ya-((Yd-Yc)/(Xd-Xc))*Xc-Yc) / (((Yb-Ya)/(Xb-Xa))-((Yd-Yc)/(Xd-Xc)))-Xa)+Ya;
            dist = Math.Sqrt(sqr(Xi - X) + sqr(Yi - Ya));
        }
        Xc = srfx[dest];
        Yc = srf[dest];
        Xd = srfx[dest + 1];
        Yd = srf[dest + 1];
        Xi = (((Yb-Ya)/(Xb-Xa))*Xa+Ya-((Yd-Yc)/(Xd-Xc))*Xc-Yc) / (((Yb-Ya)/(Xb-Xa))-((Yd-Yc)/(Xd-Xc)));
        Yi = ((Yb-Ya)/(Xb-Xa))*((((Yb-Ya)/(Xb-Xa))*Xa+Ya-((Yd-Yc)/(Xd-Xc))*Xc-Yc) / (((Yb-Ya)/(Xb-Xa))-((Yd-Yc)/(Xd-Xc)))-Xa)+Ya;
        if (dist < Math.Sqrt(sqr(Xi - X) + sqr(Yi - Ya)))
            return(true);
        return(false);
    }
    static int find_angle(int destx, int destx2, int hSpeed, int vSpeed)
    {
        int angle = 0;
        Console.Error.WriteLine(obstacle(hSpeed, vSpeed));
        double cst = 1;
        if (obstacle(hSpeed, vSpeed) == true && obstacle2(hSpeed, vSpeed) == true)
            cst = 9;
        else if (obstacle(hSpeed, vSpeed) == true || obstacle2(hSpeed, vSpeed) == true)
            cst = 2.25;
        double tmp = hSpeed;
        if (hSpeed < 0)
            tmp = -hSpeed;
        int target = destx + ((destx2 - destx) / 2);
        int i = 0;
        
        while(tmp > 20){
            tmp -= 1f;
            i++;
        }
        Console.Error.WriteLine(i);
        
        if (destx2 < X){
            if (X + (i * hSpeed) > target)
                angle = 45;
        }
        else if(destx > X){
                    Console.Error.WriteLine("BLH! = " + target + " " + (X + (i * hSpeed)));
            if (X + (i * hSpeed) < target)
                angle = -40;
        }
        else if (hSpeed < - 20)
            angle =-45;
        else if (hSpeed > 20)
            angle = 45;
/*        if (obstacle2(hSpeed, vSpeed) == true){
            if (vSpeed >= 0)
                angle = (int)(angle / cst);
            else
                angle = 0;
        }*/
        return((int)(angle / cst));
    }
    static void Main(string[] args)
    {
        string[] inputs;
        int surfaceN = int.Parse(Console.ReadLine());
        srf = new int[surfaceN];
        srfx = new int[surfaceN];
        for (int i = 0; i < surfaceN; i++)
        {
            inputs = Console.ReadLine().Split(' ');
            int landX = int.Parse(inputs[0]);
            int landY = int.Parse(inputs[1]);
            srf[i] = landY;
            srfx[i] = landX;
        }
        dest = findDest(srf);
        int angle = 0;
        int pwr = 0;
        // game loop
        while (true)
        {
            inputs = Console.ReadLine().Split(' ');
            X = int.Parse(inputs[0]);
            Y = int.Parse(inputs[1]);
            int hSpeed = int.Parse(inputs[2]);
            int vSpeed = int.Parse(inputs[3]);
            int fuel = int.Parse(inputs[4]);
            int rotate = int.Parse(inputs[5]);
            int power = int.Parse(inputs[6]);

            pwr = fallspeed(vSpeed, hSpeed, power);
            angle = find_angle(srfx[dest], srfx[dest+1], hSpeed, vSpeed);
            Console.WriteLine(angle + " " + pwr);
        }
    }
}