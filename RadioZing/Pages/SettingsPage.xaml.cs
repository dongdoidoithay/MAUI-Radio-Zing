﻿namespace RadioZing.Pages;

public partial class SettingsPage
{
    public SettingsPage(SettingsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}

