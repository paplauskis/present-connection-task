export default function PackageInfo({ pkg }) {
  return (
    <section>
      <h2>Package Info</h2>
      <p><strong>Tracking Number:</strong> {pkg.trackingNumber}</p>
      <p><strong>Status:</strong> {pkg.status}</p>
      <p><strong>Created At:</strong> {new Date(pkg.createdAt).toLocaleString()}</p>
    </section>
  );
}