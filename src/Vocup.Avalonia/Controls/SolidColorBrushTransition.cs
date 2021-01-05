using Avalonia.Animation;
using Avalonia.Media;
using System;
using System.Reactive.Linq;

namespace Vocup.Avalonia.Controls
{
    public class SolidColorBrushTransition : Transition<IBrush>
    {
        public override IObservable<IBrush> DoTransition(IObservable<double> progress, IBrush oldValue, IBrush newValue)
        {
            // This strange behavior occurs on every application shutdown with alternating null values for a few calls.
            if (oldValue is null || newValue is null)
                return progress.Select(p => new SolidColorBrush(default(Color)));

            if (oldValue is not ISolidColorBrush oldBrush)
                throw new ArgumentException("Only instances of ISolidColorBrush are supported", nameof(oldValue));
            if (newValue is not ISolidColorBrush newBrush)
                throw new ArgumentException("Only instances of ISolidColorBrush are supported", nameof(newValue));

            Color oldColor = oldBrush.Color;
            Color newColor = newBrush.Color;

            return progress.Select(p =>
            {
                double e = Easing.Ease(p);
                int A = (int)(e * (newColor.A - oldColor.A) + 0.5) + oldColor.A;
                int R = (int)(e * (newColor.R - oldColor.R) + 0.5) + oldColor.R;
                int G = (int)(e * (newColor.G - oldColor.G) + 0.5) + oldColor.G;
                int B = (int)(e * (newColor.B - oldColor.B) + 0.5) + oldColor.B;

                return new SolidColorBrush(new Color((byte)A, (byte)R, (byte)G, (byte)B));
            });
        }
    }
}
