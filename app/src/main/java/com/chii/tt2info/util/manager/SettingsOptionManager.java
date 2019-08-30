package com.chii.tt2info.util.manager;

import android.content.Context;
import android.content.SharedPreferences;

import androidx.preference.PreferenceManager;

import com.chii.tt2info.R;

/**
 * Settings option manager.
 * <p>
 * A manager that is used to manage setting options.
 */

public class SettingsOptionManager {

    private static SettingsOptionManager instance;

    public static SettingsOptionManager getInstance(Context context) {
        if (instance == null) {
            synchronized (SettingsOptionManager.class) {
                if (instance == null) {
                    instance = new SettingsOptionManager(context);
                }
            }
        }
        return instance;
    }
    private String autoNightMode;
    private String language;


    private SettingsOptionManager(Context context) {
        SharedPreferences sharedPreferences = PreferenceManager.getDefaultSharedPreferences(context);

        this.autoNightMode = sharedPreferences.getString(
                context.getString(R.string.key_auto_night_mode),
                "follow_system"
        );

        this.language = sharedPreferences.getString(
                context.getString(R.string.key_language),
                "follow_system"
        );
    }


    public String getAutoNightMode() {
        return autoNightMode;
    }

    public void setAutoNightMode(String autoNightMode) {
        this.autoNightMode = autoNightMode;
    }

    public String getLanguage() {
        return language;
    }

    public void setLanguage(String language) {
        this.language = language;
    }


}
