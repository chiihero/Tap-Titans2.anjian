package com.chii.server.tt2anjian;
import org.mybatis.spring.annotation.MapperScan;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;

@SpringBootApplication
@MapperScan("com.chii.server.tt2anjian.mapper") //添加扫描mapper
public class Tt2anjianApplication {

    public static void main(String[] args) {
        SpringApplication.run(Tt2anjianApplication.class, args);
    }

}
