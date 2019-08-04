package com.chii.server.tt2anjian.service.impl;

import com.chii.server.tt2anjian.service.InfoService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class InfoServiceImpl implements InfoService {
    private final InfoMapper infoMapper;

    @Autowired
    public InfoServiceImpl(InfoMapper infoMapper) {
        this.infoMapper = infoMapper;
    }

    @Override
    public List<Info> getInfoInfoByUsername(String username) {
        return infoMapper.selectByUserName(username);
    }

    @Override
    public int insertInfo(Info info) {
        return infoMapper.insert(info);
    }

    @Override
    public void deleteInfo(int mid) {
        infoMapper.deleteByPrimaryKey(mid);
    }
}