package com.chii.tt2info.ui.adapter;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;

import com.chii.tt2info.R;
import com.chii.tt2info.pojo.Infos;

import java.util.List;

public class InfosListViewAdapter extends BaseAdapter {
    private Context mContext;
    private List<Infos> minfoslist;

    public InfosListViewAdapter(Context mContext, List<Infos> infoslist) {
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
        TextView TV_data = ViewHolder.get(convertView, R.id.infos_data);
        TextView TV_usename = ViewHolder.get(convertView, R.id.infos_usetime);

        String layer = "层数：" + minfoslist.get(position).getLayer();
        String usetime = "运行时间：" + minfoslist.get(position).getUsetime();
        TV_data.setText(layer);
        TV_usename.setText(usetime);

        return convertView;
    }
}
