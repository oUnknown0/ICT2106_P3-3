import React from "react";

export default function BoxComponent(props) {
  const { data } = props;

  return (
    <div>
      <h3>{data.title}</h3>
      <p>{data.description}</p>
      <div className="box-content">{data.content}</div>
    </div>
  );
}
