$("#btnSave").click(function () {
  if (editBlogId == null) {
    saveData();
  } else {
    updateData();
  }
});

$("#btnCancel").click(function () {
  clearControls();
});

function getBlogInputs() {
  return {
    title: $("#txtTitle").val(),
    author: $("#txtAuthor").val(),
    content: $("#txtContent").val(),
  };
}

function clearControls() {
  $("#txtTitle").val("");
  $("#txtAuthor").val("");
  $("#txtContent").val("");
}

function saveData() {
  const blogInputs = getBlogInputs();

  const list = getData();

  const blog = {
    id: uuidv4(),
    ...blogInputs,
  };
  list.push(blog);

  const jsonStr = JSON.stringify(list);
  localStorage.setItem("blogs", jsonStr);
  clearControls();
  loadData();
}

function getData() {
  let list = localStorage.getItem("blogs");

  if (list == null) {
    list = [];
  } else {
    list = JSON.parse(list);
  }

  return list;
}

function updateData() {
  const { title, author, content } = getBlogInputs();

  let list = getData();

  const index = list.findIndex((x) => x.id == editBlogId);

  list[index].title = title;
  list[index].author = author;
  list[index].content = content;

  const jsonStr = JSON.stringify(list);
  localStorage.setItem("blogs", jsonStr);
  clearControls();
  loadData();

  editBlogId = null;
}

function loadData() {
  $("#tblBlogs").html("");
  const list = getData();

  for (let i = 0; i < list.length; i++) {
    const blog = list[i];

    const html = `<tr>
        <td>
            <button class="btn btn-warning btn-edit" data-blog-id="${
              blog.id
            }">Edit</button>
            <button class="btn btn-danger btn-delete" data-blog-id="${
              blog.id
            }">Delete</button>
        </td>
        <td>${i + 1}</td>
        <td>${blog.title}</td>
        <td>${blog.author}</td>
        <td>${blog.content}</td>
    </tr>`;
    $("#tblBlogs").append(html);

    bindEditButton();
    bindDeleteButton();
  }
}

let editBlogId = null;
function bindEditButton() {
  $(".btn-edit").click(function () {
    const id = $(this).data("blog-id");

    let list = getData();

    list = list.filter((x) => x.id == id);

    const item = list[0];

    $("#txtTitle").val(item.title);
    $("#txtAuthor").val(item.author);
    $("#txtContent").val(item.content);
    editBlogId = item.id;
  });
}

function bindDeleteButton() {
  $(".btn-delete").click(function () {
    const id = $(this).data("blog-id");

    let list = getData();

    list = list.filter((x) => x.id != id);
    const jsonStr = JSON.stringify(list);
    localStorage.setItem("blogs", jsonStr);
    loadData();
  });
}

function uuidv4() {
  return "10000000-1000-4000-8000-100000000000".replace(/[018]/g, (c) =>
    (
      +c ^
      (crypto.getRandomValues(new Uint8Array(1))[0] & (15 >> (+c / 4)))
    ).toString(16)
  );
}

$(document).ready(function () {
  loadData();
});
