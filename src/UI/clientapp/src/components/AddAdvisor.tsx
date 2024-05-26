import React, { useEffect } from "react";
import * as Yup from "yup";
import {
  Formik,
  FormikHelpers,
  FormikProps,
  Form,
  Field,
  FieldProps,
  ErrorMessage,
  FormikValues,
} from "formik";
import { IPayload } from "../models/interfaces";

interface INewAdvisorProp {
  onAddAdvisor: (payload: IPayload) => void;
}

const AddAdvisor = ({ onAddAdvisor }: INewAdvisorProp) => {
  const initialValues: IPayload = { name: "", sin: "", address: "", phone: "" };

  const validationSchema = Yup.object().shape({
    name: Yup.string().max(255, "Too Long!").required("Required"),
    sin: Yup.string().length(9, "Must be 9 characters!").required("Required"),
    address: Yup.string().max(255, "Too Long!"),
    phone: Yup.string().length(8, "Must be 8 characters!"),
  });

  return (
    <div className="add-new">
      <Formik
        initialValues={initialValues}
        validationSchema={validationSchema}
        onSubmit={onAddAdvisor}
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
            <button type="submit" disabled={isSubmitting}>
              Add New Advisor
            </button>
          </Form>
        )}
      </Formik>
    </div>
  );
};

export default AddAdvisor;
