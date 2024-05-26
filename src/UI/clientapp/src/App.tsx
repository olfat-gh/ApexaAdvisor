import React, { useState, useEffect } from "react";
import "./App.css";
import DataGrid from "./components/DataGrid";
import { IAdvisor, IPayload, IResponse } from "./models/interfaces";
import Api from "./services/api";
import { PAGE_SIZE, PAGE_INIT_INDEX } from "./constants/index";
import AddAdvisor from "./components/AddAdvisor";

function App() {
  const [loading, setLoading] = useState(false);
  const [data, setData] = useState<IResponse>();
  const [resetDataGrid, setResetDataGrid] = useState(false);

  const handleLoadData = async (pageIndex: number = PAGE_INIT_INDEX) => {
    setLoading(true);
    try {
      const response = await Api.getAdvisorsList(PAGE_SIZE, pageIndex);
      setData(response);
    } catch (error) {
      console.log(error);
    }
    setLoading(false);
    setResetDataGrid(false);
  };

  const handleDelete = async (id: string) => {
    await Api.delAdvisor(id);
    await handleLoadData(PAGE_INIT_INDEX);
  };

  return (
    <div className="App">
      <DataGrid
        totalPages={data?.totalPages || 0}
        data={data?.records}
        loading={loading}
        reset={resetDataGrid}
        onFetch={handleLoadData}
        onDelete={handleDelete}
      />
      <AddAdvisor onFetchFirstPage={() => setResetDataGrid(true)} />
    </div>
  );
}

export default App;
