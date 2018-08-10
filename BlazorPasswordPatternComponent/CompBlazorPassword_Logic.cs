using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorPasswordPatternComponent
{




    public class CompBlazorPassword_Logic : BlazorComponent
    {
        [Parameter]
        public double CompWidth { get; set; }

        [Parameter]
        public double CompHeight { get; set; }


        
        public string Password { get; set; } = string.Empty;

        public ChildLine ChildLine1 = new ChildLine();

        public Action PasswordUpdated;


        protected override void OnInit()
        {
            ComponentSettings.compWidth = CompWidth;
            ComponentSettings.compHeight = CompHeight;

            ComponentSettings.h = ComponentSettings.compWidth / ComponentSettings.CountHorizontal;

            ComponentSettings.w = ComponentSettings.compHeight / ComponentSettings.CountVertical;


            ComponentSettings.r = ComponentSettings.w;

            if (ComponentSettings.r > ComponentSettings.h)
            {
                ComponentSettings.r = ComponentSettings.h;
            }

            ComponentSettings.r = ComponentSettings.r / 2;
            ComponentSettings.r = ComponentSettings.r * 0.65;
        }

        public void Dispose()
        {

        }


        public void Clicked(string e)
        {
            if (!ComponentSettings.SelectedCircles_List.Any(x => x == e))
            {
                ComponentSettings.SelectedCircles_List.Add(e);
                //Console.WriteLine("clicked " + e);
                UpdatePassword();
                StateHasChanged();
                ChildLine1.Update();
            }
        }


        public void Reset()
        {
            ComponentSettings.SelectedCircles_List = new List<string>();
            UpdatePassword();
            StateHasChanged();
            ChildLine1.Update();
        }


        public void UnselectLast()
        {
            if (ComponentSettings.SelectedCircles_List.Count > 0)
            {
                ComponentSettings.SelectedCircles_List.Remove(ComponentSettings.SelectedCircles_List.LastOrDefault());
            }
            UpdatePassword();
            StateHasChanged();
            ChildLine1.Update();
        }





        public void UpdatePassword()
        {
            Password = string.Empty;
            foreach (var item in ComponentSettings.SelectedCircles_List)
            {

                switch (item)
                {
                    case "00":
                        Password += "7";
                        break;
                    case "10":
                        Password += "8";
                        break;
                    case "20":
                        Password += "9";
                        break;
                    case "01":
                        Password += "4";
                        break;
                    case "11":
                        Password += "5";
                        break;
                    case "21":
                        Password += "6";
                        break;
                    case "02":
                        Password += "1";
                        break;
                    case "12":
                        Password += "2";
                        break;
                    case "22":
                        Password += "3";
                        break;
                    default:
                        break;
                }

               
            }

            Console.WriteLine(Password);
            PasswordUpdated?.Invoke();



        }

    }



}
