$(document).ready(function () {
    loadTodos();

    // Tambahkan Todo baru
    $("#todoForm").submit(function (e) {
        e.preventDefault();
        let newTodo = {
            judul: $("#judul").val(),
            description: $("#description").val()
        };

        $.ajax({
            url: "/api/todo",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify(newTodo),
            success: function () {
                loadTodos();
                $("#todoForm")[0].reset();
            }
        });
    });

    function loadTodos() {
        $.ajax({
            url: "/api/todo",
            type: "GET",
            success: function (data) {
                $("#todoList").html(""); // Kosongkan tbody sebelum menambahkan data baru
                data.forEach(todo => {
                    $("#todoList").append(`
                        <tr>
                            <td>${todo.judul}</td>
                            <td>${todo.description}</td>
                            <td>
                                <button class="edit" onclick="showEditForm(${todo.id}, '${todo.judul}', '${todo.description}')">Edit</button>
                                <button class="delete" onclick="deleteTodo(${todo.id})">Hapus</button>
                            </td>
                        </tr>
                    `);
                });
            }
        });
    }
    

    window.deleteTodo = function (id) {
        $.ajax({
            url: "/api/todo/" + id,
            type: "DELETE",
            success: function () {
                loadTodos();
            }
        });
    };

    // Menampilkan form edit dengan data yang sudah ada
    window.showEditForm = function (id, judul, description) {
        $("#editId").val(id);
        $("#editJudul").val(judul);
        $("#editDescription").val(description);
        $("#editModal").show();
    };

    // Submit Edit Todo
    $("#editForm").submit(function (e) {
        e.preventDefault();
        let id = $("#editId").val();
        let updatedTodo = {
            id: id,
            judul: $("#editJudul").val(),
            description: $("#editDescription").val()
        };

        $.ajax({
            url: "/api/todo/" + id,
            type: "PUT",
            contentType: "application/json",
            data: JSON.stringify(updatedTodo),
            success: function () {
                loadTodos();
                $("#editModal").hide();
            }
        });
    });

    // Menutup modal edit
    $("#closeEditModal").click(function () {
        $("#editModal").hide();
    });
});
