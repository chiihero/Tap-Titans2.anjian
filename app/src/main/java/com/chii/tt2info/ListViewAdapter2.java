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
import com.chii.tt2info.swipe.SimpleSwipeListener;
import com.chii.tt2info.swipe.SwipeLayout;
import com.chii.tt2info.swipe.adapters.BaseSwipeAdapter;

import java.util.List;

public class ListViewAdapter2 extends BaseAdapter {

    private Context mContext;
    private List<Info> minfolist;

    ListViewAdapter2(Context mContext, List<Info> infolist ) {
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


        
        TextView tpos = (TextView)convertView.findViewById(R.id.position);
        TextView tdata = (TextView)convertView.findViewById(R.id.text_data);

        tpos.setText(String.valueOf(position + 1));
        String title = minfolist.get(position).getLayerSet().toString();
        Log.d(MainActivity.TAG, "fillValues: "+title);
        tdata.setText(title);    }
}
