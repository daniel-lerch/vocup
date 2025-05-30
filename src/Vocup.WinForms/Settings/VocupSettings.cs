﻿using LostTech.App.DataBinding;
using System;
using System.Drawing;
using System.Windows.Forms;
using Vocup.Settings.Core;

namespace Vocup.Settings;

public class VocupSettings : SettingsBase, ICopyable<VocupSettings>
{
    public VocupSettings() { }

    public VocupSettings Copy()
    {
        return new VocupSettings
        {
            _gridLines = _gridLines,
            _lastFile = _lastFile,
            _startScreen = _startScreen,
            _autoSave = _autoSave,
            _disableInternetServices = _disableInternetServices,
            _lastInternetConnection = _lastInternetConnection,
            _vhfPath = _vhfPath,
            _vhrPath = _vhrPath,
            _startupCounter = _startupCounter,
            _columnResize = _columnResize,
            _colorMode = _colorMode,
            _overrideCulture = _overrideCulture,
            _practicePercentageUnpracticed = _practicePercentageUnpracticed,
            _practicePercentageCorrect = _practicePercentageCorrect,
            _practicePercentageWrong = _practicePercentageWrong,
            _maxPracticeCount = _maxPracticeCount,
            _practiceHighlightInput = _practiceHighlightInput,
            _userEvaluates = _userEvaluates,
            _practiceFastContinue = _practiceFastContinue,
            _practiceSoundFeedback = _practiceSoundFeedback,
            _practiceShowResultList = _practiceShowResultList,
            _evaluateOptionalExpressions = _evaluateOptionalExpressions,
            _evaluateTolerateNoSynonym = _evaluateTolerateNoSynonym,
            _evaluateTolerateWhiteSpace = _evaluateTolerateWhiteSpace,
            _evaluateToleratePunctuationMark = _evaluateToleratePunctuationMark,
            _evaluateTolerateSpecialChar = _evaluateTolerateSpecialChar,
            _evaluateTolerateArticle = _evaluateTolerateArticle,
            _mainFormBounds = _mainFormBounds,
            _mainFormWindowState = _mainFormWindowState,
            _mainFormSplitterDistance = _mainFormSplitterDistance,
            _specialCharTab = _specialCharTab,
            _version = _version
        };
    }

    private bool _gridLines = true;
    [Obsolete("Grid lines cannot be switched off anymore.")]
    public bool GridLines
    {
        get => _gridLines;
        set => RaiseAndSetIfChanged(ref _gridLines, value);
    }


    private string? _lastFile;
    public string? LastFile
    {
        get => _lastFile;
        set => RaiseAndSetIfChanged(ref _lastFile, value);
    }


    private int _startScreen = 1;
    public int StartScreen
    {
        get => _startScreen;
        set => RaiseAndSetIfChanged(ref _startScreen, value);
    }


    private bool _autoSave;
    public bool AutoSave
    {
        get => _autoSave;
        set => RaiseAndSetIfChanged(ref _autoSave, value);
    }


    private bool _disableInternetServices;
    public bool DisableInternetServices
    {
        get => _disableInternetServices;
        set => RaiseAndSetIfChanged(ref _disableInternetServices, value);
    }


    private DateTime _lastInternetConnection;
    public DateTime LastInternetConnection
    {
        get => _lastInternetConnection;
        set => RaiseAndSetIfChanged(ref _lastInternetConnection, value);
    }


    private string _vhfPath = string.Empty; // value changed during startup
    public string VhfPath
    {
        get => _vhfPath;
        set => RaiseAndSetIfChanged(ref _vhfPath, value);
    }


    private string _vhrPath = string.Empty; // value changed during startup
    public string VhrPath
    {
        get => _vhrPath;
        set => RaiseAndSetIfChanged(ref _vhrPath, value);
    }


    private int _startupCounter;
    public int StartupCounter
    {
        get => _startupCounter;
        set => RaiseAndSetIfChanged(ref _startupCounter, value);
    }


    private bool _columnResize = true;
    public bool ColumnResize
    {
        get => _columnResize;
        set => RaiseAndSetIfChanged(ref _columnResize, value);
    }


#pragma warning disable WFO5001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
    private SystemColorMode _colorMode = SystemColorMode.System;
    public SystemColorMode ColorMode
    {
        get => _colorMode;
        set => RaiseAndSetIfChanged(ref _colorMode, value);
    }
#pragma warning restore WFO5001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.


    private string? _overrideCulture;
    public string? OverrideCulture
    {
        get => _overrideCulture;
        set => RaiseAndSetIfChanged(ref _overrideCulture, value);
    }

    #region Practice
    private int _practicePercentageUnpracticed = 50;
    public int PracticePercentageUnpracticed
    {
        get => _practicePercentageUnpracticed;
        set => RaiseAndSetIfChanged(ref _practicePercentageUnpracticed, value);
    }


    private int _practicePercentageCorrect = 20;
    public int PracticePercentageCorrect
    {
        get => _practicePercentageCorrect;
        set => RaiseAndSetIfChanged(ref _practicePercentageCorrect, value);
    }


    private int _practicePercentageWrong = 30;
    public int PracticePercentageWrong
    {
        get => _practicePercentageWrong;
        set => RaiseAndSetIfChanged(ref _practicePercentageWrong, value);
    }


    private int _maxPracticeCount = 3;
    public int MaxPracticeCount
    {
        get => _maxPracticeCount;
        set => RaiseAndSetIfChanged(ref _maxPracticeCount, value);
    }


    private bool _practiceHighlightInput = true;
    [Obsolete("Highlighting of input fields cannot be switched off anymore.")]
    public bool PracticeHighlightInput
    {
        get => _practiceHighlightInput;
        set => RaiseAndSetIfChanged(ref _practiceHighlightInput, value);
    }


    private bool _userEvaluates;
    public bool UserEvaluates
    {
        get => _userEvaluates;
        set => RaiseAndSetIfChanged(ref _userEvaluates, value);
    }


    private bool _practiceFastContinue;
    public bool PracticeFastContinue
    {
        get => _practiceFastContinue;
        set => RaiseAndSetIfChanged(ref _practiceFastContinue, value);
    }


    private bool _practiceSoundFeedback = true;
    public bool PracticeSoundFeedback
    {
        get => _practiceSoundFeedback;
        set => RaiseAndSetIfChanged(ref _practiceSoundFeedback, value);
    }


    private bool _practiceShowResultList = true;
    public bool PracticeShowResultList
    {
        get => _practiceShowResultList;
        set => RaiseAndSetIfChanged(ref _practiceShowResultList, value);
    }
    #endregion

    #region Evaluation
    private bool _evaluateOptionalExpressions = true;
    public bool EvaluateOptionalExpressions
    {
        get => _evaluateOptionalExpressions;
        set => RaiseAndSetIfChanged(ref _evaluateOptionalExpressions, value);
    }


    private bool _evaluateTolerateNoSynonym = true;
    public bool EvaluateTolerateNoSynonym
    {
        get => _evaluateTolerateNoSynonym;
        set => RaiseAndSetIfChanged(ref _evaluateTolerateNoSynonym, value);
    }


    private bool _evaluateTolerateWhiteSpace = true;
    public bool EvaluateTolerateWhiteSpace
    {
        get => _evaluateTolerateWhiteSpace;
        set => RaiseAndSetIfChanged(ref _evaluateTolerateWhiteSpace, value);
    }


    private bool _evaluateToleratePunctuationMark = true;
    public bool EvaluateToleratePunctuationMark
    {
        get => _evaluateToleratePunctuationMark;
        set => RaiseAndSetIfChanged(ref _evaluateToleratePunctuationMark, value);
    }


    private bool _evaluateTolerateSpecialChar = true;
    public bool EvaluateTolerateSpecialChar
    {
        get => _evaluateTolerateSpecialChar;
        set => RaiseAndSetIfChanged(ref _evaluateTolerateSpecialChar, value);
    }


    private bool _evaluateTolerateArticle = true;
    public bool EvaluateTolerateArticle
    {
        get => _evaluateTolerateArticle;
        set => RaiseAndSetIfChanged(ref _evaluateTolerateArticle, value);
    }
    #endregion

    #region UI state
    private Rectangle _mainFormBounds;
    public Rectangle MainFormBounds
    {
        get => _mainFormBounds;
        set => RaiseAndSetIfChanged(ref _mainFormBounds, value);
    }


    private FormWindowState _mainFormWindowState;
    public FormWindowState MainFormWindowState
    {
        get => _mainFormWindowState;
        set => RaiseAndSetIfChanged(ref _mainFormWindowState, value);
    }


    private int _mainFormSplitterDistance;
    public int MainFormSplitterDistance
    {
        get => _mainFormSplitterDistance;
        set => RaiseAndSetIfChanged(ref _mainFormSplitterDistance, value);
    }


    private string _specialCharTab = "de";
    public string SpecialCharTab
    {
        get => _specialCharTab;
        set => RaiseAndSetIfChanged(ref _specialCharTab, value);
    }


    Size _practiceDialogSize;
    public Size PracticeDialogSize
    {
        get => _practiceDialogSize;
        set => RaiseAndSetIfChanged(ref _practiceDialogSize, value);
    }
    #endregion

    private Version _version = new(1, 8, 3);
    public Version Version
    {
        get => _version;
        set => RaiseAndSetIfChanged(ref _version, value);
    }
}
