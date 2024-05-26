import React, { useEffect } from "react";
import * as Yup from "yup";
import {
  Formik,
  Form,
  Field,
  FieldProps,
  ErrorMessage,
  FormikHelpers,
} from "formik";
import { IPayload } from "../models/interfaces";
import Api from "../services/api";
import { Spinner } from "./Spinner";

interface INewAdvisorProp {
  onFetchFirstPage(): void;
}

const AddAdvisor = ({ onFetchFirstPage }: INewAdvisorProp) => {
  const initialValues: IPayload = { name: "", sin: "", address: "", phone: "" };

  const validationSchema = Yup.object().shape({
    name: Yup.string()
      .max(255, "Must be less than 255 characters!")
      .required("Required"),
    sin: Yup.string().length(9, "Must be 9 characters!").required("Required"),
    address: Yup.string().max(255, "Must be less than 255 characters!"),
    phone: Yup.string().length(8, "Must be 8 characters!"),
  });

  const handleSubmit = async (
    payload: IPayload,
    { setSubmitting }: FormikHelpers<IPayload>
  ) => {
    setSubmitting(true);
    try {
      await Api.addAdvisor(payload);
      onFetchFirstPage();
    } catch (error) {
      console.log(error);
    }
    setSubmitting(false);
  };

  return (
    <div className="add-new">
      <Formik
        initialValues={initialValues}
        validationSchema={validationSchema}
        onSubmit={handleSubmit}
      >
        {({ isSubmitting, errors, handleSubmit }) => (
          <Form onSubmit={handleSubmit}>
            <label htmlFor="name">Name:</label>
            <Field
              id="name"
              name="name"
              placeholder="Name"
              className={errors.name ? "error" : ""}
            />
            <ErrorMessage className="error" name="name" component="div" />
            <label htmlFor="sin">SIN:</label>
            <Field
              id="sin"
              name="sin"
              placeholder="SIN"
              className={errors.sin ? "error" : ""}
            />
            <ErrorMessage className="error" name="sin" component="div" />
            <label htmlFor="address">Address:</label>
            <Field
              id="address"
              name="address"
              placeholder="Address"
              className={errors.address ? "error" : ""}
            />
            <ErrorMessage className="error" name="address" component="div" />
            <label htmlFor="Phone">Phone:</label>
            <Field
              id="phone"
              name="phone"
              placeholder="phone"
              className={errors.phone ? "error" : ""}
            />
            <ErrorMessage className="error" name="phone" component="div" />
            {isSubmitting ? (
              <Spinner />
            ) : (
              <button type="submit">Add New Advisor</button>
            )}
          </Form>
        )}
      </Formik>
    </div>
  );
};

export default AddAdvisor;
