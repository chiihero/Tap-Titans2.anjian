package com.chii.server.tt2anjian.controller;


import com.chii.server.tt2anjian.service.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.ModelAttribute;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/test")
public class UserController {

    @GetMapping("/hello")
    public String hello(){
        System.out.println("hello");
        return "hello moto";
    }
//    @GetMapping("/user")
//    public User userinfo(@ModelAttribute("username") String username){
//        User user =userService.getUserInfoByUsername(username);
////        if (user != null) {
////            logger.info(JSONArray.toJSON(user));
////        }
////        return new DefaultResultInfo<>(user);
//        return user;
//    }
//    @GetMapping("/signout")
//    public User signout(@ModelAttribute("username") String username,@ModelAttribute("passwd") String passwd){
//        User user =userService.getUserInfoByUsername(username);
//        System.out.println(user.getUsername());
//        return user;
//    }
}
