import React, { useState, useEffect } from "react";
import { IAdvisor } from "../models/interfaces";
import "../assets/css/style.css";
import { PAGE_SIZE } from "../constants";

interface Props {
  totalPages: number;
  data: IAdvisor[] | undefined;
  loading: boolean;
  onFetch(pageIndex: number): Promise<void>;
  onDelete(id: string): void;
}

const DataGrid: React.FC<Props> = ({
  totalPages,
  data,
  loading,
  onFetch,
  onDelete,
}) => {
  const [pageIndex, setPageIndex] = useState(1);

  useEffect(() => {
    onFetch(pageIndex);
  }, [pageIndex]);

  const columns = [
    "#",
    "Name",
    "SIN",
    "Address",
    "Phone",
    "Health Status",
    "Actions",
  ];
  const pageNumbers = [];
  for (let idx = 1; idx <= totalPages; idx++)
    pageNumbers.push(
      <li key={`page-${idx}`}>
        <a
          href="#"
          className={pageIndex == idx ? "active" : ""}
          onClick={(e) => setPageIndex(idx)}
        >
          {idx}
        </a>
      </li>
    );

  return (
    <div>
      <table className="datagrid">
        <thead>
          <tr>
            {columns.map((col, index) => (
              <th key={index}>{col}</th>
            ))}
          </tr>
        </thead>
        <tbody>
          {loading ? (
            <tr>
              <td>Loading...</td>
            </tr>
          ) : data?.length === 0 ? (
            "No Data Found "
          ) : (
            data?.map((row, index) => (
              <tr key={`row-${row.id}`}>
                <td>{index + (pageIndex - 1) * PAGE_SIZE + 1}</td>
                <td>{row.name}</td>
                <td>{row.sin}</td>
                <td>{row.address}</td>
                <td>{row.phone}</td>
                <td>{row.status}</td>
                <td>
                  <a
                    href="#"
                    onClick={(e) => {
                      onDelete(row.id);
                      setPageIndex(1);
                    }}
                  >
                    Delete
                  </a>
                </td>
              </tr>
            ))
          )}
        </tbody>
      </table>
      <ul className="pagination">
        <li>
          <a href="#" onClick={(e) => setPageIndex(1)}>
            «
          </a>
        </li>
        {pageNumbers}

        <li>
          <a href="#" onClick={(e) => setPageIndex(totalPages)}>
            »
          </a>
        </li>
      </ul>
    </div>
  );
};

export default DataGrid;
