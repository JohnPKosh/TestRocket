import React from "react";
//import { Radio } from "antd";
import { submitNewMedia } from "./api";
import { withRouter } from "react-router";
import { Link } from "react-router-dom";
import { Select, message } from "antd";

const { Option } = Select;

const CreateMedia = (type) => ({ type, [type === "Book" ? "author" : "director"]: "", title: "" });

const New: React.FC<{ history; }> = ({ history }) => {

  let [collection, setCollection] = React.useState("books");

  const i_title = React.useRef(null);
  const i_credit = React.useRef(null);
  const f_add = React.useRef(null);

  React.useEffect(() => { i_title.current.focus(); }, []);

  const change = (v) => { setCollection(v); i_title.current.focus(); };
  const submit = e => {

    e.preventDefault();

    let media = CreateMedia(collection);

    if (i_title.current.value &&
      i_credit.current.value) {
      media[collection === "books" ? "author" : "director"] = i_credit.current.value;
      media.title = i_title.current.value;
    } else {
      alert("Please provide out both 'title' and 'author/director'");
      return;
    }

    submitNewMedia({ collection, media }, (status: number) => {
      switch (status) {
        case 201:
          message.success((collection === "movies" ? "Movie" : "Book") + " added successfully.", 2);
          let curCol = collection;
          f_add.current.reset();
          setCollection(curCol);
          i_title.current.focus();
          break;
        case 409:
          message.error((collection === "movies" ? "Movie" : "Book") + " already exists.", 2);
          f_add.current.reset();
          break;
        default: return ""; break;
      }

    });

  };

  return <>
    <h1>New Media</h1>
    <div>
      <form ref={f_add} method="post" action="/api/Media" onSubmit={submit}>
        <br />
        <table className="add">
          <tbody>
            <tr>
              <td>
                <strong>Type</strong>
                <br />
                <br />
              </td>
              <td>
                <Select defaultValue="books" onChange={change}>
                  <Option value={"books"}>Book</Option>
                  <Option value={"movies"}>Movie</Option>
                </Select>
                <br />
                <br />
              </td>
            </tr>
            <tr>
              <td><strong>Title</strong></td>
              <td><input ref={i_title} type="text" name="title" /></td>
            </tr>
            <tr>
              <td><strong>{collection === "books" ? "Author" : "Director"}</strong></td>
              <td><input ref={i_credit} type="text" name="credit" /></td>
            </tr>
          </tbody>
        </table>
        <br />
        <div>
          <button className="ant-button" type="submit">Submit</button>
        </div>
        <br />
        <div>
          <Link style={{ color: "royalblue" }} to="/#">Back to List</Link>
        </div>
      </form>
    </div>
  </>;
};

export default withRouter(New);