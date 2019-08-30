package com.chii.tt2info.util;

import android.content.Context;
import android.content.Intent;
import android.net.Uri;

public class IntentHelper {


    public static void startWebActivity(Context c, String url) {
        String packageName = "com.android.chrome";
        Intent browserIntent = new Intent();
        browserIntent.setPackage(packageName);
        c.startActivity(new Intent(Intent.ACTION_VIEW, Uri.parse(url)));

    }

}
