package com.chii.tt2info;

import android.app.Application;
import android.content.Context;
import android.content.SharedPreferences;
import android.content.res.Configuration;
import android.content.res.Resources;

import androidx.appcompat.app.AppCompatDelegate;

import android.util.DisplayMetrics;
import android.util.Log;

import com.chii.tt2info.ui.activity.LoginActivity;
import com.chii.tt2info.util.SPUtil;

import static androidx.appcompat.app.AppCompatDelegate.MODE_NIGHT_FOLLOW_SYSTEM;

public class MyApplication extends Application {
    public static String TAG = "MyApplication";

    @Override
    public void onCreate() {
        super.onCreate();
        initNightTheme();
    }

    public void initNightTheme() {
        Log.d(TAG, "initNightTheme: " + isNightTheme());
        if (isNightTheme()) {
            AppCompatDelegate.setDefaultNightMode(AppCompatDelegate.MODE_NIGHT_YES);
        } else {
            AppCompatDelegate.setDefaultNightMode(AppCompatDelegate.MODE_NIGHT_NO);
        }
    }

    public boolean isNightTheme() {
        return (Boolean) SPUtil.get(this, "nightTheme", Boolean.FALSE);
    }

    @Override
    public void onLowMemory() {
        super.onLowMemory();
    }
}
