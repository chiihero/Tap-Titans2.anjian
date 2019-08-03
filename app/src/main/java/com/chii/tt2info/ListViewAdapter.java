package com.chii.tt2info;

import android.content.Context;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;
import android.widget.Toast;


import com.chii.tt2info.pojo.Info;


import java.util.List;

public class ListViewAdapter extends BaseAdapter {

    private Context mContext;
    private List<Info> minfolist;

    ListViewAdapter(Context mContext, List<Info> infolist ) {
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
    public View getView(int position, View convertView, ViewGroup parent) {
        if (convertView ==null){
            convertView = LayoutInflater.from(mContext).inflate(R.layout.listview_test,null);

        }

        TextView tpos = ViewHolder.get(convertView,R.id.position);
        TextView tdata = ViewHolder.get(convertView,R.id.text_data);
//
        tpos.setText(String.valueOf(position + 1));
        String title = minfolist.get(position+1).getLayerSet().toString();
        Log.d(MainActivity.TAG, "fillValues: "+title);
        tdata.setText(title);
        return convertView;
    }
}
