﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace Vocup.Settings;

public record JsonSettings1(
    bool GridLines,
    string? LastFile,
    int StartScreen,
    bool AutoSave,
    bool DisableInternetServices,
    DateTime LastInternetConnection,
    string VhfPath,
    string VhrPath,
    int StartupCounter,
    bool ColumnResize,
#pragma warning disable WFO5001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
    SystemColorMode ColorMode,
#pragma warning restore WFO5001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
    string? OverrideCulture,
    int PracticePercentageUnpracticed,
    int PracticePercentageCorrect,
    int PracticePercentageWrong,
    int MaxPracticeCount,
    bool PracticeHighlightInput,
    bool UserEvaluates,
    bool PracticeFastContinue,
    bool PracticeSoundFeedback,
    bool PracticeShowResultList,
    bool EvaluateOptionalExpressions,
    bool EvaluateTolerateNoSynonym,
    bool EvaluateTolerateWhiteSpace,
    bool EvaluateToleratePunctuationMark,
    bool EvaluateTolerateSpecialChar,
    bool EvaluateTolerateArticle,
    Rectangle MainFormBounds,
    FormWindowState MainFormWindowState,
    int MainFormSplitterDistance,
    string SpecialCharTab,
    Size PracticeDialogSize,
    Version Version
);
