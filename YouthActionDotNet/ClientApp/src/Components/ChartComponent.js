import React from "react";

export default function ChartComponent(props) {
  const { data } = props;

  return (
    <div>
      <h3>Chart Title</h3>
      <p>Chart Description</p>
      <ul>
        {data.map((item) => (
          <li key={item.id}>
            {item.name}: {item.value}
          </li>
        ))}
      </ul>
    </div>
  );
}
