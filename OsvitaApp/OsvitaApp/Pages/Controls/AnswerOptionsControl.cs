using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using OsvitaApp.Enums;
using OsvitaApp.Helpers.Extensions;
using OsvitaApp.Models.ObservableModels;
using SkiaSharp;
using SkiaSharp.Views.Maui;
using SkiaSharp.Views.Maui.Controls;

namespace OsvitaApp.Pages.Controls;

public class AnswerOptionsControl : SKCanvasView
{
    #region Properties

    private SKCanvas _canvas;

    private SKRect _drawRect;

    private SKImageInfo _info;

    private SKPaint _primaryPaint;
    private SKPaint _correctPaint;
    private SKPaint _incorrectPaint;

    private SKFont _font;


    #region Bindable Properties

    public static readonly BindableProperty AssignmentProperty =
        BindableProperty.Create(
            nameof(Assignment),
            typeof(AssignmentObservableModel),
            typeof(AnswerOptionsControl),
            defaultValue: null,
            propertyChanged: OnAssignmentChanged);

    public AssignmentObservableModel Assignment
    {
        get => (AssignmentObservableModel)GetValue(AssignmentProperty);
        set => SetValue(AssignmentProperty, value);
    }

    private static void OnAssignmentChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (AnswerOptionsControl)bindable;

        if (oldValue is AssignmentObservableModel oldAssignment)
            oldAssignment.PropertyChanged -= control.Assignment_PropertyChanged;

        if (newValue is AssignmentObservableModel newAssignment)
            newAssignment.PropertyChanged += control.Assignment_PropertyChanged;

        control.InvalidateSurface();
    }

    private void Assignment_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        // Якщо потрібно реагувати на зміну конкретної властивості:
        if (e.PropertyName == nameof(Assignment.Answers))
        {
            // Якщо Answers — ObservableCollection, можна підписатися на CollectionChanged
            // або перемалювати все:
            InvalidateSurface();
        }

        InvalidateSurface();
    }

    public static readonly BindableProperty PrimaryColorProperty =
        BindableProperty.Create(
            nameof(PrimaryColor),
            typeof(Color),
            typeof(AnswerOptionsControl),
            Colors.Black,
            propertyChanged: OnPrimaryColorChanged);

    public static readonly BindableProperty CorrectColorProperty =
        BindableProperty.Create(
            nameof(CorrectColor),
            typeof(Color),
            typeof(AnswerOptionsControl),
            Colors.LightGreen,
            propertyChanged: OnCorrectColorChanged);

    public static readonly BindableProperty IncorrectColorProperty =
        BindableProperty.Create(
            nameof(IncorrectColor),
            typeof(Color),
            typeof(AnswerOptionsControl),
            Colors.LightPink,
            propertyChanged: OnIncorrectColorChanged);

    // Публічні властивості для прив'язки
    public Color PrimaryColor
    {
        get => (Color)GetValue(PrimaryColorProperty);
        set => SetValue(PrimaryColorProperty, value);
    }

    public Color CorrectColor
    {
        get => (Color)GetValue(CorrectColorProperty);
        set => SetValue(CorrectColorProperty, value);
    }

    public Color IncorrectColor
    {
        get => (Color)GetValue(IncorrectColorProperty);
        set => SetValue(IncorrectColorProperty, value);
    }

    private static void OnPrimaryColorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (AnswerOptionsControl)bindable;
        control._primaryPaint = new SKPaint
        {
            Color = ((Color)newValue).ToSKColor(),
            IsAntialias = true
        };
        control.InvalidateSurface();
    }

    private static void OnCorrectColorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (AnswerOptionsControl)bindable;
        control._correctPaint = new SKPaint
        {
            Color = ((Color)newValue).ToSKColor(),
            IsAntialias = true
        };
        control.InvalidateSurface();
    }

    private static void OnIncorrectColorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (AnswerOptionsControl)bindable;
        control._incorrectPaint = new SKPaint
        {
            Color = ((Color)newValue).ToSKColor(),
            IsAntialias = true
        };
        control.InvalidateSurface();
    }

    #endregion

    #endregion

    public AnswerOptionsControl()
    {
        _primaryPaint = new SKPaint() { Color = Colors.Black.ToSKColor(), IsAntialias = true };
        _correctPaint = new SKPaint() { Color = Colors.LightGreen.ToSKColor(), IsAntialias = true };
        _incorrectPaint = new SKPaint() { Color = Colors.LightPink.ToSKColor(), IsAntialias = true };
        var typeface = SKTypeface.FromFamilyName("Inter", SKFontStyle.Bold);
        _font = new SKFont(typeface, 18);
    }

    protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
    {
        base.OnPaintSurface(e);

        _canvas = e.Surface.Canvas;
        _canvas.Clear();
        _info = e.Info;
        _drawRect = new SKRect(0, 0, _info.Width, _info.Height);

        //_canvas.DrawRect(_drawRect, new SKPaint() { Color = Colors.LightBlue.ToSKColor(), Style = SKPaintStyle.Fill });

        if (Assignment is null || Assignment.Answers.Count() == 0)
        {
            return;
        }

        switch (Assignment.AssignmentModelType)
        {
            case AssignmentModelType.OneAnswerAsssignment:
                DrawOneAnswerAssigment(_canvas, _drawRect, _info, Assignment);
            break;
            default:
            break;
        }
    }

    private void DrawOneAnswerAssigment(SKCanvas canvas, SKRect drawRect, SKImageInfo info,
        AssignmentObservableModel assignment)
    {
        for (int i = 0; i < assignment.Answers.Count(); i++)
        {
            var spacing = 10f;
            var left = i * (32f + spacing) + 1f;
            var right = left + 32f - 2f;

            var answer = assignment.Answers[i];

            var answerLetterRect = new SKRect(left, 1f, right, 31f);

            var answerLetter = answer.AnswerLetter;
            var answerPaint = _primaryPaint;
            _primaryPaint.Style = SKPaintStyle.Fill;
            _canvas.DrawTextCentered(answerLetter.ToString(), answerLetterRect, _font, answerPaint);

            var answerBoxRect = new SKRect(left, 33f, right, 63f);
            _primaryPaint.Style = SKPaintStyle.Stroke;
            _primaryPaint.StrokeWidth = 1;
            _canvas.DrawRoundRect(answerBoxRect, 5, 5, answerPaint);
        }
    }
}