package com.chii.server.tt2anjian.controller;


import com.chii.server.tt2anjian.Tt2anjianApplication;

import com.chii.server.tt2anjian.service.InfoService;
import com.chii.server.tt2anjian.service.InfosService;
import com.google.gson.Gson;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.Date;
import java.util.List;


@RestController
@RequestMapping("/info")
public class InfoController {
    @Autowired
    private InfoService infoService;
    @Autowired
    private InfosService infosService;

    private static final Logger logger = LoggerFactory.getLogger(Tt2anjianApplication.class);

    @GetMapping("/getinfolist")
    public List<Info> getInfoByUsername(@ModelAttribute("username") String username) {
        List<Info> infos = infoService.getInfoInfoByUsername(username);
        logger.info(new Gson().toJson(infos));
       return infos;
    }
    @GetMapping("/getinfos")
    public List<Infos> getInfosByMid(@ModelAttribute("mid") int mid) {
        List<Infos> infos = infosService.getInfosByMid(mid);
        logger.info(new Gson().toJson(infos));
        return infos;
    }
    @PostMapping("/insert")
    public void insert(@ModelAttribute("username") String username,@ModelAttribute("title") String title){
        Info info =new Info();
        info.setUsername(username);
        info.setTitle(title);
        info.setTime(new Date());
        infoService.insertInfo(info);
    }
    @PostMapping("/insertinfo")
    public void insertinfo(@RequestBody String json){
        logger.info(json);

        Gson gson = new Gson();
        Infoslist infoslist = gson.fromJson(json,Infoslist.class);

        logger.info(new Gson().toJson(infoslist));
        Info info =new Info();
        info.setUsername(infoslist.getUsername());
        info.setTitle(infoslist.getTitle());
        info.setTime(new Date());
        int mid = infoService.insertInfo(info);

        for (Infoslist.Infos infosone: infoslist.getInfos()) {
            Infos infos = new Infos();
            infos.setMid(info.getMid());
            infos.setLayer(infosone.getLayer());
            infos.setUsetime(infosone.getUsetime());
            infosService.insertInfos(infos);
        }
    }

    @PostMapping("/delete")
    public void delete(@ModelAttribute("mid") int mid){
        infoService.deleteInfo(mid);
    }
}
