package com.chii.tt2info.util;

import android.content.Context;

import com.chii.tt2info.R;

public class ValueUtils {
    public static String getAutoNightModeName(Context c, String key) {
        switch (key) {
            case "close":
                return c.getResources().getStringArray(R.array.auto_night_mode_types)[0];

            case "auto":
                return c.getResources().getStringArray(R.array.auto_night_mode_types)[1];

            case "follow_system":
                return c.getResources().getStringArray(R.array.auto_night_mode_types)[2];

            default:
                return null;
        }
    }

    public static String getLanguageName(Context c, String key) {
        switch (key) {
            case "follow_system":
                return c.getResources().getStringArray(R.array.languages)[0];

            case "english_usa":
                return c.getResources().getStringArray(R.array.languages)[1];

            case "english_uk":
                return c.getResources().getStringArray(R.array.languages)[2];

            case "english_au":
                return c.getResources().getStringArray(R.array.languages)[3];

            case "chinese":
                return c.getResources().getStringArray(R.array.languages)[4];

            case "italian":
                return c.getResources().getStringArray(R.array.languages)[5];

            case "turkish":
                return c.getResources().getStringArray(R.array.languages)[6];

            case "german":
                return c.getResources().getStringArray(R.array.languages)[7];

            case "russian":
                return c.getResources().getStringArray(R.array.languages)[8];

            case "spanish":
                return c.getResources().getStringArray(R.array.languages)[9];

            case "japanese":
                return c.getResources().getStringArray(R.array.languages)[10];

            case "french":
                return c.getResources().getStringArray(R.array.languages)[11];

            case "portuguese_brazil":
                return c.getResources().getStringArray(R.array.languages)[12];

            default:
                return null;
        }
    }

}
