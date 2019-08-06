package com.chii.tt2info;

import android.app.Application;
import android.content.Context;
import android.content.Intent;
import android.content.res.Configuration;
import android.content.res.Resources;
import android.support.v7.app.AppCompatDelegate;
import android.util.DisplayMetrics;

import static android.support.v7.app.AppCompatDelegate.MODE_NIGHT_FOLLOW_SYSTEM;

public class MyApplication extends Application {
    private static Resources sRes;


    @Override
    public void onCreate() {
        super.onCreate();
        init(this);

    }
    public static void init(Context context) {
        sRes = context.getResources();
    }
    /**
     * 切换 夜间模式
     * @param on true 夜间， false  日间
     */
    public static void updateNightMode(boolean on) {
        DisplayMetrics dm = sRes.getDisplayMetrics();
        Configuration config = sRes.getConfiguration();
        config.uiMode &= ~Configuration.UI_MODE_NIGHT_MASK;
        config.uiMode |= on ? Configuration.UI_MODE_NIGHT_YES : Configuration.UI_MODE_NIGHT_NO;
        sRes.updateConfiguration(config, dm);
    }

    @Override
    public void onLowMemory() {
        super.onLowMemory();
    }
}
