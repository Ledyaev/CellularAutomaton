﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using CellularAutomaton.Context;
using CellularAutomaton.Context.Migrations;
using CellularAutomaton.Domain;

namespace CellularAutomaton.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var d = "asdflkjahs;dljkfha;klsehfk;aljefhncuiapoiseuhiopweiufcmahupsecm,ahiopoihesfhhhhjaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa;sldkjf;laksjdfmcjkal;slkejfcmajkl;selfijcmajisepfoicjm,asjieopfcoijfajiopsefocijmasjieopfcoiajsemfcjioapsoeifjcmjaiospefoijaseieiseieieieieieieieieiieieieieiasofiuhaspoefihposiefhcmopaishefmcpoaisehfmc111111111111111111111111111111111111111111111111111111111111oiupoihpoihpoihpoiuhpoiumnpoaiuhsemncpoaiuhmf,cpoaihsm,fcophamsefc";
            var c = "asdflkjahs;dljkfha;klsehfk;aljefhncuiapoiseuhiopweiufcmahupsecm,ahiopoihesfhhhhjaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa;sldkjf;laksjdfmcjkal;slkejfcmajkl;selfijcmajisepfoicjm,asjieopfcoijfajiopsefocijmasjieopfcoiajsemfcjioapsoeifjcmjaiospefoijaseieiseieieieieieieieieiieieieieiasofiuhaspoefihposiefhcmopaishefmcpoaisehfmc111111111111111111111111111111111111111111111111111111111111oiupoihpoihpoihpoiuhpoiumnpoaiuhsemncpoaiuhmf,cpoaihsm,fcophamsefc";
            var b = c.GetHashCode() == d.GetHashCode();
        }
    }
}
