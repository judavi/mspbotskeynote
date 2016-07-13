using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StockBot
{
    public enum SandwichOptions
    {
        Vegetariano, FiestaPeperonni, Baratisimo, Albondigas, CarneQueso, EspecialDelDia
    };
    public enum LengthOptions { Quince, Treinta };
    public enum BreadOptions { OreganoParmesano, Avena, Trigo, FinasHierbas };
    public enum CheeseOptions { Americano, Provolone };
    public enum ToppingOptions
    {
        Aguacate, Pimienta, Pepinillos, Salsa, Jalapenos,
        Aceitunas, Cebollas, Tomate
    };


    [Serializable]
    public class SandwichOrder
    {

        public SandwichOptions? Sandwich;
        public LengthOptions? Longitud;
        public BreadOptions? Pan;
        public CheeseOptions? Queso;
        public List<ToppingOptions> Adiciones;

        public static IForm<SandwichOrder> BuildForm()
        {
            return new FormBuilder<SandwichOrder>()
                    .Message("Bienvenido al bot preparador de sandwich!")
                    .Build();
        }
    }
}