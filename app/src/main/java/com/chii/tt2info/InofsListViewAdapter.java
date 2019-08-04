package com.chii.tt2info;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;

import com.chii.tt2info.pojo.Info;
import com.chii.tt2info.pojo.Infos;

import java.util.List;

public class InofsListViewAdapter extends BaseAdapter {
    private Context mContext;
    private List<Infos> minfoslist;

    InofsListViewAdapter(Context mContext, List<Infos> infoslist ) {
        this.mContext = mContext;
        this.minfoslist = infoslist;
    }

    @Override
    public int getCount() {
        return minfoslist.size();
    }

    @Override
    public Object getItem(int position) {
        return minfoslist.get(position);
    }

    @Override
    public long getItemId(int position) {
        return position;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        if (convertView == null) {
            convertView = LayoutInflater.from(mContext).inflate(R.layout.listview_infos, null);
        }


        return null;
    }
}
