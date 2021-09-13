import React from "react";
import { Link } from "react-router-dom";
import { getMedia } from "./api";
import { BookOutlined, VideoCameraOutlined, PlusOutlined, DeleteOutlined } from "@ant-design/icons";

const List: React.FC<{}> = ({ }) => {

  let [results, setResults] = React.useState([]);

  React.useEffect(() => {
    getMedia((data) => {
      setResults(data.movies.concat(data.books));
    });
  }, []);

  return <div>
    <h1>Media List</h1>
    <div>
      <div>
        {
          results.length === 0 ?
            <>
              <h2>Your collection is empty</h2>
              <h3>Let's get started. Please add a book or a movie</h3>
            </> :
            <table className="media">
              <thead>
                <tr>
                  <th>Title</th>
                  <td></td>
                  <th>Author/Director</th>
                </tr>
              </thead>
              <tbody>
                {
                  results.map((m, i) => <tr key={i}>
                    <td>{m.title}</td>
                    <td>{!!m.author ? <BookOutlined style={{ fontSize: "18px" }} /> : <VideoCameraOutlined style={{ fontSize: "18px" }} />}</td>
                    <td>{!!m.author ? m.author : m.director}</td>
                  </tr>)
                }
              </tbody>
            </table>
        }
        <br />
        <div>
          <Link to="/new" style={{ whiteSpace: "nowrap", color: "royalblue" }}>
            <PlusOutlined />
            <span> New Book/Movie</span>
          </Link>
        </div>
      </div>
    </div>
  </div>;

};

export default List;