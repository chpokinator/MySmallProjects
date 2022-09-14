package com.example.todolistappmvc.controller;


import com.example.todolistappmvc.models.ToDo;
import com.example.todolistappmvc.repository.ToDoRepo;
import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;
import org.apache.tomcat.util.http.fileupload.IOUtils;
import org.jetbrains.annotations.NotNull;
import org.springframework.boot.autoconfigure.web.ServerProperties;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.validation.BindingResult;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.multipart.MultipartFile;

import javax.annotation.Resource;
import javax.validation.Valid;
import java.awt.*;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.lang.reflect.Type;
import java.nio.charset.StandardCharsets;
import java.util.ArrayList;
import java.util.List;
import java.util.stream.Collectors;

import static org.springframework.web.bind.annotation.RequestMethod.POST;

@Controller
public class ToDoController {

    private final ToDoRepo toDoRepo;

    public ToDoController(ToDoRepo toDoRepo) {
        this.toDoRepo = toDoRepo;
    }

    @GetMapping("/")
    public String home(@NotNull Model model) {
        List<ToDo> items = toDoRepo.findAll();
        List<ToDo> todos = items.stream().filter(x -> !x.getDone()).toList();
        List<ToDo> dones = items.stream().filter(x -> x.getDone()).toList();
        model.addAttribute("todo", new ToDo());
        model.addAttribute("todos", todos);
        model.addAttribute("doneTodos", dones);
        model.addAttribute("count", items.stream().count());
        return "index";
    }

    @PostMapping("/addtodo")
    public String addToDo(@Valid ToDo toDo, BindingResult bindingResult, Model model) {
        if (bindingResult.hasErrors()) {
            return "redirect:/";
        }
        toDoRepo.save(toDo);
        return "redirect:/";
    }

    @GetMapping("/setDone/{id}")
    public String changeState(@PathVariable("id") Long id, Model model) {
        ToDo todo = toDoRepo.getById(id);
        todo.setDone(!todo.getDone());
        toDoRepo.save(todo);
        return "redirect:/";
    }

    @GetMapping("/delete/{id}")
    public String deleteTodo(@PathVariable("id") Long id, Model model) {
        ToDo todo = toDoRepo.getById(id);
        toDoRepo.delete(todo);
        return "redirect:/";
    }

    @GetMapping("/edit/{id}")
    public String editTodo(@PathVariable("id") Long id, Model model) {
        ToDo todo = toDoRepo.getById(id);
        model.addAttribute("todo", todo);
        return "update-todo";
    }

    @PostMapping("/update/{id}")
    public String updateTodo(@Valid ToDo todo, @PathVariable("id") Long id, BindingResult bindingResult, Model model) {
        if (bindingResult.hasErrors()) {
            todo.setId(id);
            return "update-todo";
        }
        toDoRepo.save(todo);
        return "redirect:/";
    }

    @GetMapping(value = "/download", produces = MediaType.APPLICATION_NDJSON_VALUE)
    public @ResponseBody
    byte[] download() {
        String json = new Gson().toJson(toDoRepo.findAll());
        return json.getBytes(StandardCharsets.UTF_8);

    }

    @PostMapping("/import")
    public String importTodos(@RequestParam("file") MultipartFile file) throws IOException {
        Gson gson = new Gson();
        String json = getJson(file.getInputStream());
        Type type = new TypeToken<List<ToDo>>(){}.getType();
        List<ToDo> read = gson.fromJson(json, type);
        toDoRepo.saveAll(read);
        return "redirect:/";
    }

    private String getJson(InputStream inputStream) throws IOException {
        return new String(inputStream.readAllBytes(), StandardCharsets.UTF_8);
    }
}
