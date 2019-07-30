package com.chii.server.tt2anjian.controller;


import com.chii.server.tt2anjian.Tt2anjianApplication;
import com.chii.server.tt2anjian.pojo.Info;
import com.chii.server.tt2anjian.pojo.Infos;
import com.chii.server.tt2anjian.pojo.User;
import com.chii.server.tt2anjian.pojo.postlist;

import com.chii.server.tt2anjian.service.InfoService;
import com.chii.server.tt2anjian.service.InfosService;
import com.chii.server.tt2anjian.service.UserService;
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
    @Autowired
    private UserService userService;

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
//    @PostMapping("/insert")
//    public void insert(@ModelAttribute("username") String username,@ModelAttribute("layerset") int layerset){
//        Info info =new Info();
//        info.setUsername(username);
//        info.setLayerSet(layerset);
//        info.setTime(new Date());
//        infoService.insertInfo(info);
//    }
    @PostMapping("/insertinfo")
    public void insertinfo(@RequestBody String json){
//        logger.info(json);
        Gson gson = new Gson();
        Info info =new Info();
        Infos infos = new Infos();

        postlist postlist = gson.fromJson(json, postlist.class);
        logger.info(new Gson().toJson(postlist));

        //验证身份
        User user = userService.getUserInfoByUsername(postlist.getUsername());
        if (user != null && user.getPasswd().equals(postlist.getPasswd())) {
            logger.info("login success");
        } else {
            return ;
        }

        info.setUsername(postlist.getUsername());
        info.setLayerSet(postlist.getLayerSet());
        info.setUpdateAll(postlist.getUpdateAll());
        info.setUpdateMini(postlist.getUpdateMini());
        info.setTime(new Date());
        infoService.insertInfo(info);
        for (postlist.Infos infosone: postlist.getInfos()) {
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
