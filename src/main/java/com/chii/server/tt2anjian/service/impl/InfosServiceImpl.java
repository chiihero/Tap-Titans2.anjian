package com.chii.server.tt2anjian.service.impl;

import com.chii.server.tt2anjian.mapper.InfosMapper;
import com.chii.server.tt2anjian.pojo.Info;
import com.chii.server.tt2anjian.pojo.Infos;
import com.chii.server.tt2anjian.service.InfoService;
import com.chii.server.tt2anjian.service.InfosService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class InfosServiceImpl implements InfosService {
    private final InfosMapper infosMapper;

    @Autowired
    public InfosServiceImpl(InfosMapper infosMapper) {
        this.infosMapper = infosMapper;
    }


    @Override
    public List<Infos> getInfosByMid(int mid) {
        return infosMapper.selectByMid(mid);
    }

    @Override
    public void insertInfos (Infos infos) {
        infosMapper.insertSelective(infos);
    }
}
