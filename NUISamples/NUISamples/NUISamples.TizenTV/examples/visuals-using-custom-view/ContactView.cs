/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */

using System;
using Tizen.NUI;
using Tizen.NUI.Constants;
using Tizen.NUI.BaseComponents;

// A ContactView is a Custom View which consists of four visuals (Image, Primitive, Text and Color).
// All of these visuals can be configured via properties - ImageURL (Image), Shape (Primitive), Name (Text) and Color.
// Tap gesture is also enabled on the ContactView which changes the color visual to some random color when ContactView is tapped.

namespace VisualsUsingCustomView
{
    public class ContactView : CustomView
    {
        private const int PROPERTY_REGISTRATION_START_INDEX = 10001000;
        private const int ColorVisualPropertyIndex = PROPERTY_REGISTRATION_START_INDEX+1 ;
        private const int PrimitiveVisualPropertyIndex = PROPERTY_REGISTRATION_START_INDEX+2;
        private const int ImageVisualPropertyIndex = PROPERTY_REGISTRATION_START_INDEX+3;
        private const int TextVisualPropertyIndex = PROPERTY_REGISTRATION_START_INDEX+4;
        private VisualBase _imageVisual;
        private VisualBase _colorVisual;
        private VisualBase _primitiveVisual;
        private VisualBase _textVisual;
        private int _shape;
        private string _imageURL;
        private string _maskURL;
        private string _name;
        private Color _color;

        static CustomView CreateInstance()
        {
            return new ContactView();
        }

        static ContactView()
        {
            ViewRegistry.Instance.Register( CreateInstance, typeof(ContactView));
        }

        public ContactView() : base(typeof(ContactView).Name, CustomViewBehaviour.RequiresKeyboardNavigationSupport)
        {
        }

        public string MaskURL
        {
            get { return _maskURL; }
            set { _maskURL=value; }
        }

        [ScriptableProperty()]
        public string ImageURL
        {
            get
            {
                return _imageURL;
            }
            set
            {
                _imageURL = value;

                // Create and Register Image Visual
                PropertyMap imageVisual = new PropertyMap();
                imageVisual.Add( Visual.Property.Type, new PropertyValue( (int)Visual.Type.Image ))
                    .Add( ImageVisualProperty.URL, new PropertyValue( _imageURL ) )
                    .Add( ImageVisualProperty.AlphaMaskURL, new PropertyValue( _maskURL ));
                _imageVisual =  VisualFactory.Get().CreateVisual( imageVisual );

                RegisterVisual( GetPropertyIndex("ImageURL"), _imageVisual );

                // Set the depth index for Image visual
                _imageVisual.DepthIndex = ImageVisualPropertyIndex;
            }
        }

        [ScriptableProperty()]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;

                // Create and Register Text Visual
                PropertyMap textVisual = new PropertyMap();
                textVisual.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Text))
                    .Add(TextVisualProperty.Text, new PropertyValue(_name))
                    .Add(TextVisualProperty.TextColor, new PropertyValue(Color.White))
                    .Add(TextVisualProperty.PointSize, new PropertyValue(7))
                    .Add( TextVisualProperty.HorizontalAlignment, new PropertyValue("CENTER"))
                    .Add( TextVisualProperty.VerticalAlignment, new PropertyValue("CENTER"));
                _textVisual =  VisualFactory.Get().CreateVisual( textVisual );

                RegisterVisual( GetPropertyIndex("Name"), _textVisual );

                // Set the depth index for Text visual
                _textVisual.DepthIndex = TextVisualPropertyIndex;
            }
        }

        [ScriptableProperty()]
        public Color Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
                BackgroundColor = value;
            }
        }

        [ScriptableProperty()]
        public int Shape
        {
            get
            {
                return _shape;
            }
            set
            {
                _shape = value;

                // Create and Register Primitive Visual
                PropertyMap primitiveVisual = new PropertyMap();
                primitiveVisual.Add( Visual.Property.Type, new PropertyValue( (int)Visual.Type.Primitive ))
                    .Add( PrimitiveVisualProperty.Shape, new PropertyValue(_shape))
                    .Add( PrimitiveVisualProperty.BevelPercentage, new PropertyValue(0.3f))
                    .Add( PrimitiveVisualProperty.BevelSmoothness, new PropertyValue(0.0f))
                    .Add( PrimitiveVisualProperty.ScaleDimensions, new PropertyValue(new Vector3(1.0f,1.0f,0.3f)))
                    .Add( PrimitiveVisualProperty.MixColor, new PropertyValue(new Vector4((245.0f/255.0f), (188.0f/255.0f), (73.0f/255.0f), 1.0f)));
                _primitiveVisual =  VisualFactory.Get().CreateVisual( primitiveVisual );
                RegisterVisual( GetPropertyIndex("Shape"), _primitiveVisual );

                // Set the depth index for Primitive visual
                _primitiveVisual.DepthIndex = PrimitiveVisualPropertyIndex;
            }
        }

        public override void OnInitialize()
        {
            // Enable Tap gesture on ContactView
            EnableGestureDetection(Gesture.GestureType.Tap);
        }

        public override void OnTap(TapGesture tap)
        {
            // Change the Color visual of ContactView with some random color
            Random random = new Random();
            float nextRed   = (random.Next(0, 256) / 255.0f);
            float nextGreen = (random.Next(0, 256) / 255.0f);
            float nextBlue  = (random.Next(0, 256) / 255.0f);
            Animation anim = AnimateBackgroundColor( new Color( nextRed, nextGreen, nextBlue, 1.0f), 0, 2000 );
            anim.Play();
        }

        public override void OnRelayout(Vector2 size, RelayoutContainer container)
        {
            // Configure the transform and size of Image visual.
            PropertyMap imageVisualTransform = new PropertyMap();
            imageVisualTransform.Add((int)VisualTransformPropertyType.Offset, new PropertyValue(new Vector2(10.0f, 0.0f)))
                .Add((int)VisualTransformPropertyType.OffsetPolicy, new PropertyValue(new Vector2((int)VisualTransformPolicyType.Absolute, (int)VisualTransformPolicyType.Absolute)))
                .Add((int)VisualTransformPropertyType.SizePolicy, new PropertyValue(new Vector2((int)VisualTransformPolicyType.Absolute, (int)VisualTransformPolicyType.Absolute)))
                .Add((int)VisualTransformPropertyType.Size, new PropertyValue(new Vector2(40.0f, 40.0f)))
                .Add((int)VisualTransformPropertyType.Origin, new PropertyValue((int)Visual.AlignType.CenterBegin))
                .Add((int)VisualTransformPropertyType.AnchorPoint, new PropertyValue((int)Visual.AlignType.CenterBegin));
            _imageVisual.SetTransformAndSize(imageVisualTransform, size);

            // Configure the transform and size of Text visual.
            PropertyMap textVisualTransform = new PropertyMap();
            textVisualTransform.Add((int)VisualTransformPropertyType.Offset, new PropertyValue(new Vector2(0.0f, 0.0f)))
                .Add((int)VisualTransformPropertyType.OffsetPolicy, new PropertyValue(new Vector2((int)VisualTransformPolicyType.Relative, (int)VisualTransformPolicyType.Relative)))
                .Add((int)VisualTransformPropertyType.SizePolicy, new PropertyValue(new Vector2((int)VisualTransformPolicyType.Absolute, (int)VisualTransformPolicyType.Absolute)))
                .Add((int)VisualTransformPropertyType.Size, new PropertyValue(new Vector2(size.X - 100.0f, 50.0f)))
                .Add((int)VisualTransformPropertyType.Origin, new PropertyValue((int)Visual.AlignType.Center))
                .Add((int)VisualTransformPropertyType.AnchorPoint, new PropertyValue((int)Visual.AlignType.Center));
            _textVisual.SetTransformAndSize(textVisualTransform, size);

            // Configure the transform and size of Primitive visual.
            PropertyMap primitiveVisualTransform = new PropertyMap();
            primitiveVisualTransform.Add((int)VisualTransformPropertyType.Offset, new PropertyValue(new Vector2(size.X - 60.0f, 0.0f)))
                .Add((int)VisualTransformPropertyType.OffsetPolicy, new PropertyValue(new Vector2((int)VisualTransformPolicyType.Absolute, (int)VisualTransformPolicyType.Absolute)))
                .Add((int)VisualTransformPropertyType.SizePolicy, new PropertyValue(new Vector2((int)VisualTransformPolicyType.Absolute, (int)VisualTransformPolicyType.Absolute)))
                .Add((int)VisualTransformPropertyType.Size, new PropertyValue(new Vector2(40.0f, 40.0f)))
                .Add((int)VisualTransformPropertyType.Origin, new PropertyValue((int)Visual.AlignType.CenterBegin))
                .Add((int)VisualTransformPropertyType.AnchorPoint, new PropertyValue((int)Visual.AlignType.CenterBegin));
            _primitiveVisual.SetTransformAndSize(primitiveVisualTransform, size);
        }
    }
}
