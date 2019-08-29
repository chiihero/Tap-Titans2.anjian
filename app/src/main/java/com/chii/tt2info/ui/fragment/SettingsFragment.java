package com.chii.tt2info.ui.fragment;

import android.content.SharedPreferences;
import android.os.Bundle;
import android.widget.RadioGroup;

import androidx.annotation.Nullable;
import androidx.preference.ListPreference;
import androidx.preference.Preference;
import androidx.preference.PreferenceFragmentCompat;
import androidx.preference.PreferenceManager;

import com.chii.tt2info.R;
import com.chii.tt2info.util.ValueUtils;
import com.chii.tt2info.util.manager.SettingsOptionManager;

public class SettingsFragment extends PreferenceFragmentCompat implements Preference.OnPreferenceChangeListener {
    @Override
    public void onCreatePreferences(Bundle savedInstanceState, String rootKey) {

    }

    @Override
    public void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        addPreferencesFromResource(R.xml.preference_setting);
        if (getActivity() != null) {
            SharedPreferences sharedPreferences = PreferenceManager.getDefaultSharedPreferences(getActivity());
            initBasicPart(sharedPreferences);
        }
    }

    private void initBasicPart(SharedPreferences sharedPreferences) {
        //auto day night
        ListPreference autoDayNigth = findPreference(getString(R.string.key_auto_night_mode));
        String autoDayNigthValue = sharedPreferences.getString(getString(R.string.key_auto_night_mode), "Follow system");
        String autoDayNigthName = ValueUtils.getAutoNightModeName(getActivity(), autoDayNigthValue);
        autoDayNigth.setSummary(getString(R.string.now) + " : " + autoDayNigthName);
        autoDayNigth.setOnPreferenceChangeListener(this);
        // TODO: 2019/8/16
        Preference nightStartTime = findPreference(getString(R.string.key_night_start_time));
        nightStartTime.setSummary(getString(R.string.now) + " : ");
        if (autoDayNigthValue.equals("auto")) {
            nightStartTime.setEnabled(true);
        } else {
            nightStartTime.setEnabled(false);
        }
        Preference nightEndTime = findPreference(getString(R.string.key_night_end_time));
        nightEndTime.setSummary(getString(R.string.now) + " : ");
        if (autoDayNigthValue.equals("auto")) {
            nightStartTime.setEnabled(true);
        } else {
            nightStartTime.setEnabled(false);
        }
    }

    @Override
    public boolean onPreferenceChange(Preference preference, Object newValue) {
        if (preference.getKey().equals(getString(R.string.key_auto_night_mode))) {
            // auto night mode.
            SettingsOptionManager.getInstance(getActivity()).setAutoNightMode((String) newValue);
            String autoNightMode = ValueUtils.getAutoNightModeName(getActivity(), (String) newValue);
            preference.setSummary(getString(R.string.now) + " : " + autoNightMode);
            if (((String) newValue).equals("auto")) {
                findPreference(getString(R.string.key_night_start_time)).setEnabled(true);
                findPreference(getString(R.string.key_night_end_time)).setEnabled(true);
            } else {
                findPreference(getString(R.string.key_night_start_time)).setEnabled(false);
                findPreference(getString(R.string.key_night_end_time)).setEnabled(false);
            }
        } else if (preference.getKey().equals(getString(R.string.key_language))) {
            // language.
            SettingsOptionManager.getInstance(getActivity()).setLanguage((String) newValue);
            String language = ValueUtils.getLanguageName(getActivity(), (String) newValue);
            preference.setSummary(getString(R.string.now) + " : " + language);
            showRebootSnackbar();
        }
        return true;
    }

    private void showRebootSnackbar() {
        // TODO: 2019/8/17
    }
}
