﻿using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Wordprocessing;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drawing = DocumentFormat.OpenXml.Drawing;


namespace ppt_lib
{
    internal class shapeList
    {

        public static Shape TitleShape(int y, HtmlNode htmlNode)
        {

            //drawingObjectId++;
            Shape titleShape = new Shape();
            // Specify the required shape properties for the title shape. 
            titleShape.NonVisualShapeProperties = new NonVisualShapeProperties
                (new NonVisualDrawingProperties() { Id = processSlidesAdd.drawingObjectId2, Name = "Title" },
                new NonVisualShapeDrawingProperties(new Drawing.ShapeLocks() { NoGrouping = true }),
                new ApplicationNonVisualDrawingProperties(new PlaceholderShape() { Type = PlaceholderValues.Title }));
            titleShape.ShapeProperties = new ShapeProperties()
            {
                Transform2D = new Drawing.Transform2D(
                                          new Drawing.Offset() { X = 0, Y = 2900000 },
                                         //new Drawing.Offset() { X = 0, Y = y },
                                         new Drawing.Extents() { Cx = 9144000, Cy = 557200 }
                                         )
            };

            ///add header alone
            ///
            int fontSize()
            {
                switch (htmlNode.Name)
                {
                    case "h1":
                        return 6500;
                    case "h2":
                        return 5500;
                    case "h3":
                        return 5000;
                    case "h4":
                        return 4000;
                    default:
                        return 6500;
                }
            };
            //set the font size 


            // Specify the text of the title shape.
            titleShape.TextBody = new TextBody(new Drawing.BodyProperties(),
                    new Drawing.ListStyle()
                    );


            TextBodyProcess(titleShape.TextBody, htmlNode, fontSize());
            return titleShape;
        }
        public static Shape TextShape(int y, HtmlNode htmlNode)
        {


            //set the font size 
            // Declare and instantiate the body shape of the new slide.
            Shape bodyShape = new Shape();

            // Specify the required shape properties for the body shape.
            bodyShape.NonVisualShapeProperties = new NonVisualShapeProperties(new NonVisualDrawingProperties() { Id = processSlidesAdd.drawingObjectId2, Name = "Content Placeholder" },
                    new NonVisualShapeDrawingProperties(new Drawing.ShapeLocks() { NoGrouping = true }),
                    new ApplicationNonVisualDrawingProperties(new PlaceholderShape() { Index = 1 }));
            bodyShape.ShapeProperties = new ShapeProperties()
            {
                Transform2D = new Drawing.Transform2D(
                                         new Drawing.Offset() { X = 0, Y = y },
                                         new Drawing.Extents() { Cx = 9144000, Cy = 457200 }
                                         )
            };

            Drawing.Paragraph Slide = new Drawing.Paragraph();

            // Specify the text of the title shape.
            bodyShape.TextBody = new TextBody(new Drawing.BodyProperties(),
                    new Drawing.ListStyle()
                    );
            TextBodyProcess(bodyShape.TextBody,htmlNode, 1800);


            return bodyShape;
        }

        public static Shape BulletListShape(int y, HtmlNode htmlNode)
        {


            //set the font size 
            // Declare and instantiate the body shape of the new slide.
            Shape listShape = new Shape();
            Drawing.Paragraph para = new Drawing.Paragraph(new Drawing.ParagraphProperties() { Alignment = Drawing.TextAlignmentTypeValues.Center });

            // Specify the required shape properties for the body shape.
            listShape.NonVisualShapeProperties = new NonVisualShapeProperties(new NonVisualDrawingProperties() { Id = processSlidesAdd.drawingObjectId2, Name = "Content Placeholder" },
                    new NonVisualShapeDrawingProperties(new Drawing.ShapeLocks() { NoGrouping = true }),
                    new ApplicationNonVisualDrawingProperties(new PlaceholderShape() { Index = 1 }));
            listShape.ShapeProperties = new ShapeProperties()
            {
                Transform2D = new Drawing.Transform2D(
                                         new Drawing.Offset() { X = 0, Y = y },
                                         new Drawing.Extents() { Cx = 9144000, Cy = 457200 }
                                         )
            };

            /*
             <a:pPr marL="285750" indent="-285750" xmlns:a="http://schemas.openxmlformats.org/drawingml/2006/main">
            <a:buFont typeface="Arial" panose="020B0604020202020204" pitchFamily="34" charset="0" />
            <a:buChar char="•" />
            </a:pPr><a:r xmlns:a="http://schemas.openxmlformats.org/drawingml/2006/main">
            <a:rPr lang="en-US" dirty="0" />
            <a:t>Element1</a:t>
            </a:r>
             */
            string textList = "";
            int lastNode = htmlNode.ChildNodes.Count - 2;
            int index = 0;
            foreach (var list in htmlNode.ChildNodes)
            {
                if (index == lastNode && list.Name == "li")
                {
                    /*if (list.HasChildNodes)
                    {
                        foreach (var liItem in list.ChildNodes)
                        {
                            if (liItem.Name == "a")
                            {
                                string href = liItem.Attributes["href"].Value;
                                string text = liItem.InnerText;
                                HyperLInkElement hyperlink = processSlidesAdd.UrlProcess(href);
                                string tooltip = liItem.Attributes["title"]?.Value != null ? liItem.Attributes["title"]?.Value : "";

                           
                                para.AppendChild(
                                            new Drawing.Run(
                                            new Drawing.RunProperties(
                                                new Drawing.SolidFill(
                                                    new Drawing.RgbColorModelHex() { Val = "A0AABF" }
                                                    ),
                                                new Drawing.EffectList()
                                                , new Drawing.HyperlinkOnClick() { Id = hyperlink.id, Tooltip = tooltip }
                                                )
                                            { Language = "en-US", Dirty = false, Bold = true, FontSize = 18000},
                                            new Drawing.Text() { Text = liItem.InnerText })
                                            );
                            }
                        }
                    }
                    else
                    {*/
                        textList += list.InnerText;
                        processSlidesAdd.y += 600000;

                    //}
                    
                }
                else if (list.Name == "li")
                {
                    //procesar 

                    textList += list.InnerText + "\n";
                    processSlidesAdd.y += 300000;


                }
                index++;
            }


            // Specify the text of the title shape.
            listShape.TextBody = new TextBody(new Drawing.BodyProperties(),
                    new Drawing.ListStyle(),

                    new Drawing.Paragraph(
                        new Drawing.ParagraphProperties(
                            new Drawing.BulletFont() { Typeface = "Arial", Panose = "020B0604020202020204", PitchFamily = 34, CharacterSet = 0 },
                            new Drawing.CharacterBullet() { Char = "•" }
                        )
                        { Alignment = Drawing.TextAlignmentTypeValues.Center, LeftMargin = 285750, Indent = -285750 },
                        new Drawing.Run(
                         new Drawing.RunProperties() { Language = "en-US", Dirty = false, SpellingError = false, FontSize = 1800 },
                        new Drawing.Text() { Text = textList })
                                        )
                    );
            return listShape;
        }

        public static Shape OrderedListShape(int y, HtmlNode htmlNode)
        {


            //set the font size 
            // Declare and instantiate the body shape of the new slide.
            Shape listShape = new Shape();

            // Specify the required shape properties for the body shape.
            listShape.NonVisualShapeProperties = new NonVisualShapeProperties(new NonVisualDrawingProperties() { Id = processSlidesAdd.drawingObjectId2, Name = "Content Placeholder" },
                    new NonVisualShapeDrawingProperties(new Drawing.ShapeLocks() { NoGrouping = true }),
                    new ApplicationNonVisualDrawingProperties(new PlaceholderShape() { Index = 1 }));
            listShape.ShapeProperties = new ShapeProperties()
            {
                Transform2D = new Drawing.Transform2D(
                                         new Drawing.Offset() { X = 0, Y = y },
                                         new Drawing.Extents() { Cx = 9144000, Cy = 457200 }
                                         )
            };

            /*
            <a:pPr marL="342900" indent="-342900" xmlns:a="http://schemas.openxmlformats.org/drawingml/2006/main">
            <a:buFont typeface="+mj-lt" />
            <a:buAutoNum type="arabicPeriod" />
            </a:pPr>
            <a:r xmlns:a="http://schemas.openxmlformats.org/drawingml/2006/main">
            <a:rPr lang="en-US" dirty="0" />
            <a:t>One
            </a:t>
            </a:r>
             */
            string textList = "";
            int lastNode = htmlNode.ChildNodes.Count - 2;
            int index = 0;
            foreach (var list in htmlNode.ChildNodes)
            {
                if (index == lastNode && list.Name == "li")
                {
                    textList += list.InnerText;
                    processSlidesAdd.y += 600000;
                }
                else if (list.Name == "li")
                {
                    textList += list.InnerText + "\n";
                    processSlidesAdd.y += 300000;
                }
                index++;
            }

            // Specify the text of the title shape.
            listShape.TextBody = new TextBody(new Drawing.BodyProperties(),
                    new Drawing.ListStyle(),

                    new Drawing.Paragraph(
                        new Drawing.ParagraphProperties(
                            new Drawing.BulletFont() { Typeface = "Arial", Panose = "020B0604020202020204", PitchFamily = 34, CharacterSet = 0 },
                            new Drawing.AutoNumberedBullet() { Type = Drawing.TextAutoNumberSchemeValues.ArabicPeriod }
                        )
                        { Alignment = Drawing.TextAlignmentTypeValues.Center, LeftMargin = 285750, Indent = -285750 },
                        new Drawing.Run(
                         new Drawing.RunProperties() { Language = "en-US", Dirty = false, SpellingError = false, FontSize = 1800 },
                        new Drawing.Text() { Text = textList })
                                        )
                    );
            return listShape;
        }


        public static Shape codeblockShape(int y, HtmlNode htmlNode)
        {

            //set the font size 
            // Declare and instantiate the body shape of the new slide.
            Shape bodyShape = new Shape();

            ///
            /// 
            /// <a:xfrm xmlns:a="http://schemas.openxmlformats.org/drawingml/2006/main">
            /// <a:off x="500000" y="1064008" />
            /// <a:ext cx="8144000" cy="923330" />
            /// </a:xfrm>
            /// <a:prstGeom prst="rect" xmlns:a="http://schemas.openxmlformats.org/drawingml/2006/main">
            /// <a:avLst />
            /// </a:prstGeom>
            /// <a:solidFill xmlns:a="http://schemas.openxmlformats.org/drawingml/2006/main">
            /// <a:schemeClr val="bg1">
            /// <a:lumMod val="65000" />
            /// </a:schemeClr>
            /// </a:solidFill>
            /// 


            // Specify the required shape properties for the body shape.
            bodyShape.NonVisualShapeProperties = new NonVisualShapeProperties(new NonVisualDrawingProperties() { Id = processSlidesAdd.drawingObjectId2, Name = "Content Placeholder" },
                    new NonVisualShapeDrawingProperties(new Drawing.ShapeLocks() { NoGrouping = true }),
                    new ApplicationNonVisualDrawingProperties(new PlaceholderShape() { Index = 1 }));
            bodyShape.ShapeProperties = new ShapeProperties(
                new Drawing.PresetGeometry(new Drawing.AdjustValueList()) { Preset = Drawing.ShapeTypeValues.Rectangle }
                ,
                new Drawing.SolidFill(new DocumentFormat.OpenXml.Drawing.SchemeColor(new Drawing.LuminanceModulation() { Val = 65000 }) { Val = Drawing.SchemeColorValues.Background1 })


                )
            {

                Transform2D = new Drawing.Transform2D(
                                         new Drawing.Offset() { X = 500000, Y = y },
                                         new Drawing.Extents() { Cx = 8144000, Cy = 923330 }
                                         )

            };


            // Specify the text of the title shape.
            bodyShape.TextBody = new TextBody(new Drawing.BodyProperties() { Wrap = Drawing.TextWrappingValues.Square },
                    new Drawing.ListStyle(),
                    new Drawing.Paragraph(
                        new Drawing.ParagraphProperties() { Alignment = Drawing.TextAlignmentTypeValues.Left },
                        new Drawing.Run(
                         new Drawing.RunProperties() { Language = "en-US", Dirty = false, SpellingError = false, FontSize = 1800 },
                        new Drawing.Text() { Text = htmlNode.InnerText })
                                        )
                    );

            return bodyShape;

        }

       
        public static Shape BlockQuoteShape(int y, HtmlNode htmlNode)
        {

            //set the font size 
            // Declare and instantiate the body shape of the new slide.
            Shape bodyShape = new Shape();

            ///
            /// 
            /// <a:xfrm xmlns:a="http://schemas.openxmlformats.org/drawingml/2006/main">
            /// <a:off x="500000" y="1064008" />
            /// <a:ext cx="8144000" cy="923330" />
            /// </a:xfrm>
            /// <a:prstGeom prst="rect" xmlns:a="http://schemas.openxmlformats.org/drawingml/2006/main">
            /// <a:avLst />
            /// </a:prstGeom>
            /// <a:solidFill xmlns:a="http://schemas.openxmlformats.org/drawingml/2006/main">
            /// <a:schemeClr val="bg1">
            /// <a:lumMod val="65000" />
            /// </a:schemeClr>
            /// </a:solidFill>
            /// 


            // Specify the required shape properties for the body shape.
            bodyShape.NonVisualShapeProperties = new NonVisualShapeProperties(
                new NonVisualDrawingProperties() { Id = processSlidesAdd.drawingObjectId2, Name = "Content Placeholder" },
                    new NonVisualShapeDrawingProperties(new Drawing.ShapeLocks() { NoGrouping = true }),
                    new ApplicationNonVisualDrawingProperties(new PlaceholderShape() { Index = 1 })
                    )
            ;

            bodyShape.ShapeProperties = new ShapeProperties(
                new Drawing.PresetGeometry(new Drawing.AdjustValueList()) { Preset = Drawing.ShapeTypeValues.Rectangle }
                /* ,
                 new Drawing.SolidFill(new DocumentFormat.OpenXml.Drawing.SchemeColor(new Drawing.LuminanceModulation() { Val = 65000 }) { Val = Drawing.SchemeColorValues.Background1 })*/
                )
            {

                Transform2D = new Drawing.Transform2D(
                                         new Drawing.Offset() { X = 500000, Y = y },
                                         new Drawing.Extents() { Cx = 8144000, Cy = 923330 }
                                         )

            };


            // Specify the text of the title shape.
            bodyShape.TextBody = new TextBody(new Drawing.BodyProperties(new Drawing.ShapeAutoFit()) { Wrap = Drawing.TextWrappingValues.Square, RightToLeftColumns = false },
                    new Drawing.ListStyle(),
                    new Drawing.Paragraph(
                        new Drawing.ParagraphProperties() { Level = 1 },
                        new Drawing.Run(
                         new Drawing.RunProperties() { Language = "en-US", Dirty = false, SpellingError = false, FontSize = 1800 },
                        new Drawing.Text() { Text = htmlNode.InnerText })
                        ,
                        new Drawing.EndParagraphRunProperties()
                                        )
                    );

            return bodyShape;

        }

        public static void TextBodyProcess(TextBody textBody, HtmlNode htmlNode,int FontSize)
        {
            
            Drawing.Paragraph para = new Drawing.Paragraph(new Drawing.ParagraphProperties() { Alignment = Drawing.TextAlignmentTypeValues.Center });
            
            //"Love<strong>is</strong>bold but don't go to far\nthe ice cream man appear on a van\nwith <em>all</em> flavors\nto try"
            foreach (var htmlNodeChild in htmlNode.ChildNodes)
            {
                
                //Until finds \n 
                // '#text' "strong" "em"
                if (htmlNodeChild.Name == "#text")
                {
                    //split n 
                    //create a Run for each one of the elements
                    if (htmlNodeChild.InnerText.Contains("\n"))
                    {
                        string[] text = htmlNodeChild.InnerText.Split('\n');
                        int i = 0;
                        foreach (string lines in text)
                        {
                            
                            // \n happens
                            
                            if (i ==text.Length-1)
                            {
                                para.AppendChild(
                                new Drawing.Run(
                                new Drawing.RunProperties() { Language = "en-US", Dirty = false, SpellingError = false, FontSize = FontSize },
                                new Drawing.Text() { Text = lines })
                                );

                            }
                            else
                            {
                                para.AppendChild(
                                new Drawing.Run(
                                new Drawing.RunProperties() { Language = "en-US", Dirty = false, SpellingError = false, FontSize = FontSize },
                                new Drawing.Text() { Text = lines })
                                );
                                
                                textBody.AppendChild(para);
                                para = new Drawing.Paragraph(new Drawing.ParagraphProperties() { Alignment = Drawing.TextAlignmentTypeValues.Center });

                            }
                            i++;
                        }//here ends loop

                    }
                    else
                    {
                        para.AppendChild(
                                new Drawing.Run(
                                new Drawing.RunProperties() { Language = "en-US", Dirty = false, SpellingError = false, FontSize = FontSize },
                                new Drawing.Text() { Text = htmlNodeChild.InnerText })
                                );
                    
                    }

                }
                else if (htmlNodeChild.Name == "code")
                {
                    para.AppendChild(
                                new Drawing.Run(
                                new Drawing.RunProperties(
                                    new Drawing.Highlight(new Drawing.RgbColorModelHex() { Val= "C0C0C0" })
                                    ) { Language = "en-US", Dirty = false, Bold = true, FontSize = FontSize },
                                new Drawing.Text() { Text = htmlNodeChild.InnerText })
                                );
                }
                else if(htmlNodeChild.Name == "strong") 
                {
                    para.AppendChild(
                                new Drawing.Run(
                                new Drawing.RunProperties() { Language = "en-US", Dirty = false, Bold=true, FontSize = FontSize },
                                new Drawing.Text() { Text = htmlNodeChild.InnerText })
                                );
                }
                else if (htmlNodeChild.Name == "a")
                {
                    //href
                    string href = htmlNodeChild.Attributes["href"].Value;
                    string text = htmlNodeChild.InnerText;
                    HyperLInkElement hyperlink =processSlidesAdd.UrlProcess(href);
                    //here add the link 
                    //Name: "title", Value: "The best search engine for privacy"
                    string tooltip = htmlNodeChild.Attributes["title"]?.Value!=null ? htmlNodeChild.Attributes["title"]?.Value: "" ;

                    /*
                      <a:rPr lang="en-US" b="0" i="0" u="sng" dirty="0">
                                <a:solidFill>
                                    <a:srgbClr val="A0AABF"/>
                                </a:solidFill>
                                <a:effectLst/>
                                <a:latin typeface="Georgia" panose="02040502050405020303" pitchFamily="18" charset="0"/>
                                <a:hlinkClick r:id="rId2" tooltip="The best search engine for privacy"/>
                            </a:rPr>
                     */
                    para.AppendChild(
                                new Drawing.Run(
                                new Drawing.RunProperties(
                                    new Drawing.SolidFill( 
                                        new Drawing.RgbColorModelHex() { Val= "A0AABF" }
                                        ),
                                    new Drawing.EffectList() 
                                    ,new Drawing.HyperlinkOnClick() { Id= hyperlink.id, Tooltip= tooltip}
                                    ) { Language = "en-US", Dirty = false, Bold = true, FontSize = FontSize },
                                new Drawing.Text() { Text = htmlNodeChild.InnerText })
                                );
                }
                else if (htmlNodeChild.Name == "em") {
                    
                    if (htmlNodeChild.FirstChild.Name== "strong")
                    {
                        para.AppendChild(
                                    new Drawing.Run(
                                    new Drawing.RunProperties() { Language = "en-US", Dirty = false, Bold = true, Italic = true, FontSize = FontSize },
                                    new Drawing.Text() { Text = htmlNodeChild.InnerText })
                                    );
                    }
                    else
                    {
                        para.AppendChild(
                                    new Drawing.Run(
                                    new Drawing.RunProperties() { Language = "en-US", Dirty = false, Italic = true, FontSize = FontSize },
                                    new Drawing.Text() { Text = htmlNodeChild.InnerText })
                                    );
                    }
                    
                }

               
            }//here ends loop

            textBody.AppendChild(para);
         
        }
    
      
    }
}
