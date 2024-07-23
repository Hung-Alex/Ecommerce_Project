import { useContext, useState } from "react";
import { useForm } from "react-hook-form";
import { toast } from "react-hot-toast";
import axios from "../../../utils/axios";
import { BrandContext } from "../../../context/BrandContext"; // Make sure to create and set up BrandContext
import { useNavigate } from "react-router-dom";

const AddBrandForm = () => {
  const [loading, setLoading] = useState(false);
  const { register, handleSubmit } = useForm();
  const { setBrand } = useContext(BrandContext); // Use the BrandContext to manage state
  const navigate = useNavigate();

  const notify = () => toast.success("Brand added successfully");

  const onSubmit = async (data) => {
    setLoading(true);
    try {
      const formData = new FormData();
      formData.append("name", data.name);
      formData.append("description", data.description);
      formData.append("urlSlug", data.urlSlug);
      formData.append("formFile", data.formFile[0]); // Assuming formFile is a file input

      const response = await axios.post("/brands", formData, {
        headers: {
          "Content-Type": "multipart/form-data",
        },
      });

      if (response.data) {
        notify();
        setBrand(response.data.data);
        navigate("/admin/brands");
        setLoading(false);
      }
    } catch (error) {
      setLoading(false);
    }
  };

  return (
    <div className="border-t-2 border-[#7eb693] shadow-md rounded bg-white max-w-2xl mb-24">
      <h3 className="text-xl text-center my-4 p-3">Brand Details</h3>
      <form onSubmit={handleSubmit(onSubmit)}>
        <div className="mx-6 pb-6 grid grid-cols-1 md:grid-cols-12 gap-4">
          <div className="col-span-6">
            <label className="block mb-2" htmlFor="name">
              Name
            </label>
            <input
              {...register("name", { required: true })}
              className="block outline-none p-2 border rounded bg-slate-50 w-full text-gray-600"
              type="text"
              placeholder="Brand Name"
              required
            />
          </div>
          <div className="col-span-6">
            <label className="block mb-2" htmlFor="description">
              Description
            </label>
            <input
              {...register("description")}
              className="block outline-none p-2 border rounded bg-slate-50 w-full text-gray-600"
              type="text"
              placeholder="Description"
            />
          </div>
        </div>
        <div className="mx-6 pb-6 grid grid-cols-1 md:grid-cols-12 gap-4">
          <div className="col-span-6">
            <label className="block mb-2" htmlFor="urlSlug">
              Url Slug
            </label>
            <input
              {...register("urlSlug")}
              className="block outline-none p-2 border rounded bg-slate-50 w-full text-gray-600"
              type="text"
              placeholder="URL Slug"
            />
          </div>
          <div className="col-span-6">
            <label className="block mb-2" htmlFor="formFile">
              Form File
            </label>
            <input
              {...register("formFile", { required: true })}
              className="block outline-none p-2 border rounded bg-slate-50 w-full text-gray-600"
              type="file"
              required
            />
          </div>
        </div>
        <div className="mx-6 pb-6 text-right">
          <input
            className="bg-[#7eb693] text-white p-1 px-4 rounded cursor-pointer"
            type="submit"
            value={loading ? "Loading..." : "Add"}
          />
        </div>
      </form>
    </div>
  );
};

export default AddBrandForm;
