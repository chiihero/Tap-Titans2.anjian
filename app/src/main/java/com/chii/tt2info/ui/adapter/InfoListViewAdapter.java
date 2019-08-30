package com.chii.tt2info.ui.adapter;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;


import com.chii.tt2info.R;
import com.chii.tt2info.pojo.Info;
import com.chii.tt2info.util.TimeUtil;

import java.util.List;

public class InfoListViewAdapter extends BaseAdapter {

    private Context mContext;
    private List<Info> minfolist;

    public InfoListViewAdapter(Context mContext, List<Info> infolist) {
        this.mContext = mContext;
        this.minfolist = infolist;
    }
    @Override
    public int getCount() {
        return minfolist.size();
    }

    @Override
    public Object getItem(int position) {
        return minfolist.get(position);
    }

    @Override
    public long getItemId(int position) {
        return position;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup viewGroup) {
        if (convertView == null) {
            convertView = LayoutInflater.from(mContext).inflate(R.layout.listview_infolist, null);
        }
        TextView TV_title = ViewHolder.get(convertView, R.id.info_title);
        TextView TV_notes = ViewHolder.get(convertView, R.id.info_notes);
        TextView TV_layerSet = ViewHolder.get(convertView, R.id.info_layerSet);
        TextView TV_updateAll = ViewHolder.get(convertView, R.id.info_updateAll);
        TextView TV_updateMini = ViewHolder.get(convertView, R.id.info_updateMini);
        TextView TV_time = ViewHolder.get(convertView, R.id.info_time);


        String title =minfolist.get(position).getTitle();
        String layerSet = "层数设定："+minfolist.get(position).getLayerSet().toString();
        String notes ="备注："+minfolist.get(position).getNotes();
        String updateAll = "全面升级次数："+minfolist.get(position).getUpdateAll().toString();
        String updateMini = "小升级次数："+minfolist.get(position).getUpdateMini().toString();
        String timestamp = minfolist.get(position).getTime();
        //2019-08-05 09:22:27.0/
        //获取.之前的字符串
        String time = TimeUtil.String2Data(timestamp).toString().split("\\.")[0];

        TV_title.setText(title);
        TV_notes.setText(notes);
        TV_layerSet.setText(layerSet);
        TV_updateAll.setText(updateAll);
        TV_updateMini.setText(updateMini);
        TV_time.setText(time);
        return convertView;
    }
}
