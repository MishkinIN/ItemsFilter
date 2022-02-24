// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;
using Northwind.NET.Sample;


namespace Shazzam.Shaders {
	
	/// <summary>An effect that turns the input into shades of a single color.</summary>
	public class MonochromeEffect : ShaderEffect {
		public static readonly DependencyProperty InputProperty = ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(MonochromeEffect), 0);
		public static readonly DependencyProperty FilterColorProperty = DependencyProperty.Register("FilterColor", typeof(Color), typeof(MonochromeEffect), new UIPropertyMetadata(Colors.Gray, PixelShaderConstantCallback(0)));
		public MonochromeEffect() {
			PixelShader pixelShader = new PixelShader();
			//pixelShader.UriSource = new Uri("/Northwind.NET.Sample;component/Shaders/MonochromeEffect.ps", UriKind.Relative);
			var assemblyShortName = typeof(MonochromeEffect).Assembly.ToString().Split(',')[0];
			pixelShader.UriSource = new Uri($"pack://application:,,,/{assemblyShortName};component/Shaders/MonochromeEffect.ps");
			this.PixelShader = pixelShader;

			this.UpdateShaderValue(InputProperty);
			this.UpdateShaderValue(FilterColorProperty);
		}
		public Brush Input {
			get {
				return ((Brush)(this.GetValue(InputProperty)));
			}
			set {
				this.SetValue(InputProperty, value);
			}
		}
		/// <summary>The color used to tint the input.</summary>
		public Color FilterColor {
			get {
				return ((Color)(this.GetValue(FilterColorProperty)));
			}
			set {
				this.SetValue(FilterColorProperty, value);
			}
		}
	}
}
