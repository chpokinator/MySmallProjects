package com.example.todolistappmvc.repository;

import com.example.todolistappmvc.models.ToDo;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

public interface ToDoRepo extends JpaRepository<ToDo, Long> {
}
