﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ContaPagar
    {
        public int Id;
        public string Nome;
        public decimal Valor;
        public string Tipo;
        public DateTime Data_Vencimento;
        public bool Fechada;
    }
}
