using BlazorSvgHelper;
using BlazorSvgHelper.Classes.SubClasses;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.RenderTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BlazorPasswordPatternComponent
{
    public class CompBlazorPasswordPattern : BlazorComponent
    {

        [Parameter]
        public double CompWidth { get; set; }

        [Parameter]
        public double CompHeight { get; set; }

        //[Parameter]
        //public int ActualValue { get; set; }

        //[Parameter]
        //public Action<int> ActualValueChanged { get; set; }

        //[Parameter]
        //public string ForeColor1 { get; set; }

        //[Parameter]
        //public string ForeColor2 { get; set; }

        svg _Svg = null;


        Action act1 = null;


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


        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {

          

            //ComponentSettings.radius_2_Times = WidthAndHeight;


            //ComponentSettings.radius_Origin = WidthAndHeight / 2;
            //ComponentSettings.radius_BigCircle = ComponentSettings.radius_Origin * 0.9;
            //ComponentSettings.radius_SmallCircle = ComponentSettings.radius_Origin * 0.6;
            //ComponentSettings.CircleWidth = ComponentSettings.radius_BigCircle - ComponentSettings.radius_SmallCircle;

            // builder.AddElementReferenceCapture

            Generate_SVG();

            //builder.OpenElement(0, "button");
            //builder.AddAttribute(1, "width", "250");
            //builder.AddAttribute(2, "height", "250");
            //builder.AddAttribute(1, "onclick", act1("5"));


            //builder.AddAttribute(1, "onclick", () =>
            //{
            //    Abc("aaaa");
            //});

            //builder.AddContent(2, "vashli");
            //builder.CloseElement();

            Cmd_Render(_Svg, 0, builder);


            base.BuildRenderTree(builder);
        }



        public void Generate_SVG()
        {
            _Svg = new svg
            {
                id = "svgPasswordPattern",
                width = CompWidth,
                height = CompHeight,
                xmlns = "http://www.w3.org/2000/svg",
            };


            _Svg.Children.Add(new rect
            {
                width = CompWidth,
                height = CompWidth,
                fill = ComponentSettings.BG_Color,
                stroke = "blue",
                stroke_width = CompWidth * 0.01
            });




            AddCircles();

            
        }


        public void AddCircles()
        {
            

          


            int k = 0;
            for (int i = 0; i < ComponentSettings.CountHorizontal; i++)
            {
                for (int j = 0; j < ComponentSettings.CountVertical; j++)
                {
                    k++;
                    _Svg.Children.Add(new circle
                    {
                        id = "circle" + k,
                        cx = i * ComponentSettings.w + ComponentSettings.w * 0.5,
                        cy = j * ComponentSettings.h + ComponentSettings.h * 0.5,
                        r = ComponentSettings.r,
                        stroke_width = CompWidth * 0.05,
                        stroke = ComponentSettings.BigCircle_Color,
                        fill = ComponentSettings.BigCircle_Color,
                        onclick = i+" "+j,
                    });


                    _Svg.Children.Add(new circle
                    {
                        cx = i * ComponentSettings.w + ComponentSettings.w * 0.5,
                        cy = j * ComponentSettings.h + ComponentSettings.h * 0.5,
                        r = ComponentSettings.r * 0.4,
                        fill = ComponentSettings.SmallCircle_Color,
                    });


                  //  return;

                }
            }



        }


        public void Cmd_Render<T>(T _Item, int k, RenderTreeBuilder builder)
        {


            Action<UIMouseEventArgs> act1 = new Action<UIMouseEventArgs>((s) => {
                Abc(s);
            });

            object _value;

            string _attrName = string.Empty;

            bool IsAllowed = true;


            builder.OpenElement(k++, _Item.GetType().Name);


            foreach (PropertyInfo pi in _Item.GetType().GetProperties().Where(x => !x.PropertyType.Name.Contains("ICollection")))
            {
                IsAllowed = true;
       


                _value = pi.GetValue(_Item, null);
               


                if (pi.PropertyType == typeof(double))
                {
                    if (double.IsNaN((double)_value))
                    {
                        IsAllowed = false;

                    }
                    else
                    {
                        _value = Math.Round((double)_value, 2);
                    }
                }

                if (IsAllowed)
                {
                    IsAllowed = _value != null && !string.IsNullOrEmpty(_value.ToString());
                }


                if (IsAllowed)
                {
                    _attrName = pi.Name;


                    if (_attrName.Equals("onclick"))
                    {
                      
                        builder.AddAttribute(k++, _attrName, act1);

                       
                    }
                    else
                    {
                        if (_attrName.Equals("content"))
                        {
                            builder.AddContent(k++, _value.ToString());
                      
                        }
                        else
                        {
                            if (_attrName.Contains("_"))
                            {
                                _attrName = _attrName.Replace("_", "-");
                            }

                            //if (_attrName.Contains("99"))
                            //{
                            //    _attrName = _attrName.Replace("99", ":");
                            //}

                            builder.AddAttribute(k++, _attrName, _value.ToString());
                    
                        }

                    }
                }
            }


            PropertyInfo pi_Children = _Item.GetType().GetProperty("Children");

            if (pi_Children != null)
            {
                List<object> children = pi_Children.GetValue(_Item) as List<object>;

                foreach (object item in children)
                {
                    Cmd_Render(item, k, builder);
                }
            }

            builder.CloseElement();
      

        }


        public void Abc(UIMouseEventArgs a)
        {
          

            Console.WriteLine("I was invoked " + a.ClientX);

            Console.WriteLine("I was invoked " + a.ClientY);
        }





        public void Dispose()
        {

        }
    }
}
